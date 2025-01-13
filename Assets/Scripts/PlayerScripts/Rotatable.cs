using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class Rotatable : IRotatable
    {
        private const float MinRotationDirectionThreshold = 0.1f;
        private const float MaxAimAngleThreshold = 65f;
        private const float MinConstraintWeight = 0f;
        private const float MaxConstraintWeight = 1f;

        private readonly Transform _transform;
        private readonly MultiAimConstraint _multiAimConstraint;
        private readonly float _rotationSpeed;
        private readonly float _ikRotationSpeed;

        public Rotatable(Transform transform, MultiAimConstraint multiAimConstraint,
            float rotationSpeed, float ikRotationSpeed)
        {
            _transform = transform;
            _multiAimConstraint = multiAimConstraint;
            _rotationSpeed = rotationSpeed;
            _ikRotationSpeed = ikRotationSpeed;
        }

        public void Rotate(Vector3 targetPosition)
        {
            if (Time.timeScale == 0)
            {
                return;
            }

            Vector3 direction = (targetPosition - _transform.position).normalized;
            if (direction.magnitude < MinRotationDirectionThreshold) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            float angleDifference = Quaternion.Angle(_transform.rotation, targetRotation);

            if (Mathf.Abs(angleDifference) < MaxAimAngleThreshold)
            {
                _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, MaxConstraintWeight, 
                    Time.deltaTime * _ikRotationSpeed);
            }
            else
            {
                _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, MinConstraintWeight, 
                    Time.deltaTime * _ikRotationSpeed);
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation,
                    _rotationSpeed * Time.deltaTime);
            }
        }
    }
}