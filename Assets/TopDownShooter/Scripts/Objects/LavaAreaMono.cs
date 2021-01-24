using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using TopDownShooter.Stat;
using UnityEngine;

namespace TopDownShooter.Objects
{
    public class LavaAreaMono : MonoBehaviour, IDamage
    {
        [SerializeField] private float _damage;
        public float Damage { get { return _damage; } }
        [Range(0.1f, 2)]
        [SerializeField] private float _armorPenetration = 3;
        public float ArmorPenetration { get { return _armorPenetration; } }

        [SerializeField] private float _timeBaseDamage = 3;
        public float TimedBaseDamage { get { return _timeBaseDamage; } }

        [SerializeField] private float _timeBaseDamageDuration = 3;
        public float TimedBaseDamageDuration { get { return _timeBaseDamageDuration; } }

        public PlayerStat Stat { get { return null; } }

        private void OnTriggerEnter(Collider collider)
        {
            int colliderInstanceId = collider.GetInstanceID();
            if (DamagebleHelper.DamagebleList.ContainsKey(colliderInstanceId))
            {
                DamagebleHelper.DamagebleList[colliderInstanceId].Damage(this);
            }
        }
    }
}