using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Inventory
{
    public class PlayerInventoryCannonItemMono : AbstractPlayerInventoryItemMono
    {
        [SerializeField] private Transform _cannonShootPoint;
        public void Shoot()
        {
            //add also effects and such
            ScriptableShootManager.Instance.Shoot(_cannonShootPoint.position, _cannonShootPoint.forward);
        }
    }
}