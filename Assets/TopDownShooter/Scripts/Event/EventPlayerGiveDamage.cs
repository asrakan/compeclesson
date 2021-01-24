using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Stat
{
    public struct EventPlayerGiveDamage
    {
        public float Damage;
        public PlayerStat ReceiverStat;
        public PlayerStat ShooterStat;
        public EventPlayerGiveDamage(float damage, PlayerStat receiverStat,PlayerStat shooterStat)
        {
            ReceiverStat = receiverStat;
            Damage = damage;
            ShooterStat = shooterStat;
        }
    }
}