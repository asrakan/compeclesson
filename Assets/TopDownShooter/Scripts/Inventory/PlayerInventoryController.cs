using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TopDownShooter.Stat;

namespace TopDownShooter.Inventory
{
    public class PlayerInventoryController : MonoBehaviour, IPlayerStatHolder
    {
        [SerializeField] private AbstractBasePlayerInventoryItemData[] _inventoryItemDataArray;
        private List<AbstractBasePlayerInventoryItemData> _instantiatedItemDataList;
        public Transform BodyParent;
        public Transform CannonParent;

        public ReactiveCommand ReactiveShootCommand { get; private set; }
        public PlayerStat PlayerStat { get; private set; }

        private void Start()
        {
            //FOR TESTING PURPOSES ONLY
            InitializeInventory(_inventoryItemDataArray);
        }

        private void OnDestroy()
        {
            ClearInventory();
        }


        public void InitializeInventory(AbstractBasePlayerInventoryItemData[] inventoryItemDataArray)
        {
            if (ReactiveShootCommand != null)
            {
                //adjusting reactive command
                ReactiveShootCommand.Dispose();
            }
            ReactiveShootCommand = new ReactiveCommand();

            //clearing old inventory and creating new one
            ClearInventory();
            _instantiatedItemDataList = new List<AbstractBasePlayerInventoryItemData>(inventoryItemDataArray.Length);
            for (int i = 0; i < inventoryItemDataArray.Length; i++)
            {
                var instantiated = Instantiate(inventoryItemDataArray[i]);
                instantiated.Initialize(this);
                _instantiatedItemDataList.Add(instantiated);
            }
        }

        private void ClearInventory()
        {
            if (_instantiatedItemDataList != null)
            {
                for (int i = 0; i < _instantiatedItemDataList.Count; i++)
                {
                    _instantiatedItemDataList[i].Destroy();
                }
            }
        }

        public void SetStat(PlayerStat stat)
        {
            PlayerStat = stat;
        }
    }
}
