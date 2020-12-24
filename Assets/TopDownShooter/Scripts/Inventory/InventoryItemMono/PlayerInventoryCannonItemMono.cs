using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Inventory
{
    public class PlayerInventoryCannonItemMono : AbstractPlayerInventoryItemMono
    {
        [SerializeField] private Transform _cannonShootPoint;
        public void Shoot(IDamage damage)
        {
            //add also effects and such
            ScriptableShootManager.Instance.Shoot(_cannonShootPoint.position, _cannonShootPoint.forward, damage);
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