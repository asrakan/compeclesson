using System.Collections;
using System.Collections.Generic;
using TopDownShooter.PlayerInput;
using UnityEngine;

namespace TopDownShooter.AI
{
    public class InputDataAI : AbstractInputData
    {
        protected Vector3 _currentTarget;
        protected Transform _targetTransform;


        public void SetTarget(Transform targetTransform, Vector3 target)
        {
            _targetTransform = targetTransform;
            _currentTarget = target;
        }

        public override void ProcessInput()
        {
            
        }
    }
}