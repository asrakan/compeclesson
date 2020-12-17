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
        [SerializeField] private PlayerMovementController _playerMovementController;
        [SerializeField] private PlayerInventoryController _inventoryController;
        [SerializeField] private TowerRotationController _playerTowerRotationController;


        public Transform TARGET;
        private void Awake()
        {
            //creating new one
            _aiMovementInput = Instantiate(_aiMovementInput);
            _aiRotationInput = Instantiate(_aiRotationInput);

            _playerMovementController.InitializeInput(_aiMovementInput);
            _playerTowerRotationController.InitializeInput(_aiRotationInput);



        }

        private void Update()
        {


            _aiMovementInput.SetTarget(transform, TARGET.position);
            _aiRotationInput.SetTarget(transform, TARGET.position);
            

            _aiMovementInput.ProcessInput();
            _aiRotationInput.ProcessInput();


        }
    }
}