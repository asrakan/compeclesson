using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using UnityEngine;


namespace TopDownShooter.Stat
{
    public static class DamagebleHelper
    {
        public static Dictionary<int,IDamageble> DamagebleList = new Dictionary<int,IDamageble>();
        public static void InitializeDamageble(this IDamageble damageble)
        {
            DamagebleList.Add(damageble.InstanceId, damageble);
        }

        public static void DestroyDamageble(this IDamageble damageble)
        {
            DamagebleList.Remove(damageble.InstanceId);
        }
    }


    public interface IDamageble
    {
        int InstanceId { get; }
        void Damage(IDamage dmg);
    }
}