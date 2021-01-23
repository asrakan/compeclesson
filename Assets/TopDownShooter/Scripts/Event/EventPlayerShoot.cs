using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Inventory
{
    public struct EventPlayerShoot
    {
        public Vector3 Origin;
        public int ShooterId;
        public EventPlayerShoot(Vector3 origin,int shooterId)
        {
            Origin = origin;
            ShooterId = shooterId;
        }
    }
}