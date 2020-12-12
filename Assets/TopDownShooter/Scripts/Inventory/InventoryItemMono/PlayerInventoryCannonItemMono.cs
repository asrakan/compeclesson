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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_cannonShootPoint.position, .15f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_cannonShootPoint.position, _cannonShootPoint.position + _cannonShootPoint.forward * 10);
        }
    }
}