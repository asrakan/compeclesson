using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Inventory
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Inventory/Player Inventory Body Item Data")]
    public class PlayerInventoryBodyItemData : AbstractPlayerInventoryItemData<PlayerInventoryBodyItemMono>
    {
        public override void Initialize(PlayerInventoryController targetPayerInventory)
        {
            var instantiated = InstantiateAndInitializePrefab(targetPayerInventory.Parent);
            Debug.Log("THIS CLASS IS PLAYER INVENTORY BODY ITEM DATA");
        }
    }
}