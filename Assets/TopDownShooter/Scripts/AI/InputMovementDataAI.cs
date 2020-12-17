using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDownShooter.AI
{
    [CreateAssetMenu(menuName = "Topdown Shooter/Input/AI/Movement Input Data")]
    public class InputMovementDataAI : InputDataAI
    {
        public Vector3 rotationDebug;
        public Vector3 rotationDebugLocal;
        public float rotationGapDebug;
        public override void ProcessInput()
        {
            float distance = Vector3.Distance(_targetTransform.position, _currentTarget);
            if (distance > 0)
            {
                Vertical = 1;
            }
            else
            {
                Vertical = 0;
            }
            Vector3 dir = _currentTarget - _targetTransform.position;
            var rotation = Quaternion.LookRotation(dir, Vector3.up).eulerAngles;
            rotationDebug = rotation;
            rotationDebugLocal = _targetTransform.rotation.eulerAngles;
            var rotationGap = Mathf.Abs(rotation.y - _targetTransform.rotation.eulerAngles.y);
            rotationGapDebug = rotationGap;
            if (Mathf.Abs(rotationGap) > 5f)//0.25f 
            {
                float horizontalClamped = Mathf.Clamp(rotationGap / 180, -1, 1);
                Horizontal = horizontalClamped;
            }
            else
            {
                Horizontal = 0;
            }
        }
    }
}