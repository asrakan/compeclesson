using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;

        private void Update()
        {
            CameraMovementFollow();
            CameraRotationFollow();
        }

        private void CameraRotationFollow()
        {
            _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, Quaternion.LookRotation(_targetTransform.forward), Time.deltaTime * _cameraSettings.RotationLerpSpeed);
        }


        private void CameraMovementFollow()
        {
            Vector3 offset = (_cameraTransform.right * _cameraSettings.PositionOffset.x)+ ( _cameraTransform.up * _cameraSettings.PositionOffset.y)+(_cameraTransform.forward * _cameraSettings.PositionOffset.z);
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetTransform.position + offset, Time.deltaTime * _cameraSettings.PositionLerp);
        }

    }
}