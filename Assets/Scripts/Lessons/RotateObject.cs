using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;

        private void Update()
        {
            transform.Rotate(Vector3.up, _speed * Time.deltaTime);
            ///100 frame per second 100 * 0.1=10  //////////// 50 frame per second 50*.2 = 10
        }

    }
}