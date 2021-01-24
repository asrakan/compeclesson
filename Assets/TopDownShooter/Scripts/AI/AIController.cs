using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using TopDownShooter.PlayerControls;
using TopDownShooter.PlayerInput;
using UnityEngine;
using UniRx;
using System;

namespace TopDownShooter.AI
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private InputDataAI _aiMovementInput;
        [SerializeField] private InputDataAI _aiRotationInput;
        [SerializeField] private InputDataAI _towerRotationInput;
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private PlayerInventoryController _inventoryController;
        [SerializeField] private TowerRotationController _playerTowerRotationController;

        public List<AITarget> TargetList;
        private Vector3 _targetMovementPosition;
        private CompositeDisposable _targetDispose;
        private void Start()
        {
            //creating new one
            _aiMovementInput = Instantiate(_aiMovementInput);
            _aiRotationInput = Instantiate(_aiRotationInput);
            _towerRotationInput = Instantiate(_towerRotationInput);


            _playerMovementController.InitializeInput(_aiMovementInput);
            _playerTowerRotationController.InitializeInput(_towerRotationInput);

            UpdateTarget();

        }

        private void UpdateTarget()
        {
            var position = transform.position;
            _targetMovementPosition = position + ((TargetList[0].transform.position - position).normalized * (Vector3.Distance(TargetList[0].transform.position, position) - 10));



            _aiMovementInput.SetTarget(transform, _targetMovementPosition);
            _aiRotationInput.SetTarget(transform, _targetMovementPosition);
            _towerRotationInput.SetTarget(_playerTowerRotationController.TowerTransform, TargetList[0].transform.position);

            _targetDispose = new CompositeDisposable();
            TargetList[0].PlayerStat.OnDeath.Subscribe(OnTargetDeath).AddTo(_targetDispose);

        }

        private void OnTargetDeath(Unit obj)
        {
            Debug.Log("target is dead");
            _targetDispose.Dispose();
            TargetList.RemoveAt(0);
            if (TargetList.Count > 0)
            {
                UpdateTarget();
            }
            else
            {
                this.enabled = false;
            }
        }

        private void Update()
        {
            _aiMovementInput.ProcessInput();
            _aiRotationInput.ProcessInput();
            _towerRotationInput.ProcessInput();


            if (_towerRotationInput.Horizontal < 2 && Vector3.Distance(_targetMovementPosition, transform.position) < 5)
            {
                _inventoryController.ReactiveShootCommand.Execute();
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_targetMovementPosition, 1);
        }
    }
}