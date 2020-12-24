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
            if (Armor > 0)
            {
                Armor -= (dmg.Damage * dmg.ArmorPenetration);
            }
            else
            {
                Health -= dmg.Damage;
                //basic if armor is negative means damage is much more than armor
                Health += Armor;
                if (Health <= 0)
                {
                    OnDeath.Execute();
                    Destroy(gameObject);
                }
            }
        }
    }
}