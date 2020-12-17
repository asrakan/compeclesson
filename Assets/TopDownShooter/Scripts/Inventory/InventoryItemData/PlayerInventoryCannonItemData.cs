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
            InstantiateAndInitializePrefab(targetPayerInventory.CannonParent);
            targetPayerInventory.ReactiveShootCommand.Subscribe(OnReactiveShootCommand).AddTo(_compositeDisposable);
        }


        public override void Destroy()
        {
            base.Destroy();
        }

        private void OnReactiveShootCommand(Unit obj)
        {
            Shoot();
        }

        public void Shoot()
        {
            if (Time.time - _lastShootTime > _rpm)
            {
                _instantiated.Shoot();
                _lastShootTime = Time.time;
            }
        }
    }
}