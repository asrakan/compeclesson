using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Requests
{
    public class CameraRotationController : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _lerpSpeed = 3;
        private void Update()
        {
            _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation, Quaternion.LookRotation(_targetTransform.position- _cameraTransform.position), Time.deltaTime * _lerpSpeed);
        }
    }
}