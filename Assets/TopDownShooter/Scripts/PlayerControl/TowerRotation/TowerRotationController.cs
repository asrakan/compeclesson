using System.Collections;
using System.Collections.Generic;
using TopDownShooter.PlayerInput;
using UnityEngine;


namespace TopDownShooter.PlayerControls
{
    public class TowerRotationController : MonoBehaviour
    {
        [SerializeField] private AbstractInputData _rotationInput;
        [SerializeField] private Transform _towerTransform;
        public Transform TowerTransform { get { return _towerTransform; } }
        [SerializeField] private TowerRotationSettings _towerRotationSettings;
        public void InitializeInput(AbstractInputData inputData)
        {
            _rotationInput = inputData;
        }

        private void Update()
        {
            _towerTransform.Rotate(0, _rotationInput.Horizontal * _towerRotationSettings.TowerRotationSpeed, 0, Space.Self);
        }
    }
}