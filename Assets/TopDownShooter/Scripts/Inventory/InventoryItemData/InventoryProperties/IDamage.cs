using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Inventory
{
    public interface IDamage
    {
        float Damage { get; }
        float ArmorPenetration { get; }


        float TimedBaseDamageDuration { get; }
        float TimedBaseDamage { get; }
    }
}