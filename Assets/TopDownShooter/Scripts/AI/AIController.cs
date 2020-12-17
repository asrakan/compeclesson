using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using TopDownShooter.PlayerControls;
using TopDownShooter.PlayerInput;
using UnityEngine;

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


        public Transform _movementTarget;
        public Transform _towerTarget;
        private void Awake()
        {
            //creating new one
            _aiMovementInput = Instantiate(_aiMovementInput);
            _aiRotationInput = Instantiate(_aiRotationInput);
            _towerRotationInput = Instantiate(_towerRotationInput);


            _playerMovementController.InitializeInput(_aiMovementInput);
            _playerTowerRotationController.InitializeInput(_towerRotationInput);



        }

        private void Update()
        {


            _aiMovementInput.SetTarget(transform, _movementTarget.position);
            _aiRotationInput.SetTarget(transform, _movementTarget.position);
            _towerRotationInput.SetTarget(_playerTowerRotationController.TowerTransform, _towerTarget.position);


            _aiMovementInput.ProcessInput();
            _aiRotationInput.ProcessInput();
            _towerRotationInput.ProcessInput();

           // _inventoryController.ReactiveShootCommand.Execute();

        }
    }
}