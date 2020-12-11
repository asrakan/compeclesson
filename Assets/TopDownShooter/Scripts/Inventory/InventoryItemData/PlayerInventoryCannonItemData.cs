using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.Inventory
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Inventory/Player Inventory Cannon Item Data")]
    public class PlayerInventoryCannonItemData : AbstractPlayerInventoryItemData<PlayerInventoryCannonItemMono>
    {
        [SerializeField] private float _damage;
        public float Damage { get { return _damage; } }
        public override void CreateIntoInventory(PlayerInventoryController targetPayerInventory)
        {
            var instantiated = InstantiateAndInitializePrefab(targetPayerInventory.Parent);
            //bunun cannono props
            Debug.Log("THIS CLASS IS PLAYER INVENTORY CANNON ITEM DATA");
        }
    }
}