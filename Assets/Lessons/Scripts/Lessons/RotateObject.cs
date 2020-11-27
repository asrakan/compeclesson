using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private RotateSettings _rotateSettings;
        private void Update()
        {
            transform.Rotate(Vector3.up, _rotateSettings.Speed * Time.deltaTime);
            ///100 frame per second 100 * 0.1=10  //////////// 50 frame per second 50*.2 = 10
        }

    }
}