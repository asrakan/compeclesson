using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Inventory;
using TopDownShooter.PlayerInput;
using UnityEngine;

namespace TopDownShooter
{

    public class LocalPlayerController : PlayerController
    {
        [SerializeField] private AbstractInputData _shootInput;

        private void Update()
        {
            if (_shootInput.Horizontal > 0)
            {
                _inventoryController.ReactiveShootCommand.Execute();
            }
        }
    }
}