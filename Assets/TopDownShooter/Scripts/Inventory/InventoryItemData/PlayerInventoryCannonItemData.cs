using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace TopDownShooter.Inventory
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Inventory/Player Inventory Cannon Item Data")]
    public class PlayerInventoryCannonItemData : AbstractPlayerInventoryItemData<PlayerInventoryCannonItemMono>
    {
        [SerializeField] private float _damage;
        public float Damage { get { return _damage; } }
        [SerializeField] private float _rpm = 1f;
        public float RPM { get { return _rpm; } }
        private float _lastShootTime;

        public override void Initialize(PlayerInventoryController targetPayerInventory)
        {
            base.Initialize(targetPayerInventory);
            InstantiateAndInitializePrefab(targetPayerInventory.Parent);
            targetPayerInventory.ReactiveShootCommand.Subscribe(OnReactiveShootCommand).AddTo(_compositeDisposable);
            //bunun cannono props
            Debug.Log("THIS CLASS IS PLAYER INVENTORY CANNON ITEM DATA");
        }


        public override void Destroy()
        {
            base.Destroy();
        }

        private void OnReactiveShootCommand(Unit obj)
        {
            Debug.Log("reactive command shoot");
            Shoot();
        }

        public void Shoot()
        {
            if (Time.time - _lastShootTime > _rpm)
            {
                _instantiated.Shoot();
                _lastShootTime = Time.time;
            }
            else
            {
                Debug.LogError("you can't shoot now");
            }
        }
    }
}