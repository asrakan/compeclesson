using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;

        private void Update()
        {
            CameraRotationFollow();
            CameraMovementFollow();
        }

        private void CameraRotationFollow()
        {
            _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, Quaternion.LookRotation(_targetTransform.position - _cameraTransform.position), Time.deltaTime * _cameraSettings.RotationLerpSpeed);
        }


        private void CameraMovementFollow()
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetTransform.position + _cameraSettings.PositionOffset, Time.deltaTime * _cameraSettings.PositionLerp);
        }

    }
}