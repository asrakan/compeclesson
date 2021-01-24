using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TopDownShooter.Inventory;

namespace TopDownShooter.Stat
{
    public class DamagebleObjectBase : MonoBehaviour, IDamageble, IPlayerStatHolder
    {
        [SerializeField] private Collider _collider;
        public int InstanceId { get; private set; }

        public PlayerStat PlayerStat { get; set; }

        private Vector3 _defaultScale;
        private bool _isDead = false;

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
            else
            {
                PlayerStat.Damage(dmg);
            }
        }


        private IEnumerator TimedBaseDamage(float damage, float totalDuration)
        {
            while (totalDuration > 0)
            {
                yield return new WaitForSeconds(1);
                totalDuration -= 1;
                PlayerStat.Damage(damage);
            }
        }

        public void SetStat(PlayerStat stat)
        {
            PlayerStat = stat;
        }
    }
}