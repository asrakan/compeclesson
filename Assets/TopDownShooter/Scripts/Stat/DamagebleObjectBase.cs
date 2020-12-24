using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TopDownShooter.Inventory;

namespace TopDownShooter.Stat
{
    public class DamagebleObjectBase : MonoBehaviour, IDamageble
    {
        [SerializeField] private Collider _collider;
        public int InstanceId { get; private set; }
        public float Health = 100;
        public float Armor = 100;
        private Vector3 _defaultScale;
        private bool _isDead = false;
        public ReactiveCommand OnDeath = new ReactiveCommand();
        protected virtual void Awake()
        {
            InstanceId = _collider.GetInstanceID();
            this.InitializeDamageble();
            _defaultScale = transform.localScale;
        }

        protected virtual void Destroy()
        {
            this.DestroyDamageble();
        }

        public virtual void Damage(IDamage dmg)
        {
            if (dmg.TimedBaseDamage > 0)
            {
                StartCoroutine(TimedBaseDamage(dmg.TimedBaseDamage, dmg.TimedBaseDamageDuration));
            }
            if (Armor > 0)
            {
                Armor -= (dmg.Damage * dmg.ArmorPenetration);
            }
            else
            {
                Health -= dmg.Damage;
                //basic if armor is negative means damage is much more than armor
                Health += Armor;
                CheckHealth();
            }
        }


        private IEnumerator TimedBaseDamage(float damage, float totalDuration)
        {
            while (totalDuration > 0)
            {
                yield return new WaitForSeconds(1);
                totalDuration -= 1;
                Health -= damage;
                CheckHealth();
            }
        }

        private void CheckHealth()
        {
            if (_isDead)
            {
                return;
            }
            if (Health <= 0)
            {
                StopAllCoroutines();
                _isDead = true;
                OnDeath.Execute();
                Destroy(gameObject);
            }
        }
    }
}