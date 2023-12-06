using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Rotatable : IRotatable
{
    private readonly Transform _transform;
    private readonly MultiAimConstraint _multiAimConstraint;
    private readonly float _rotationSpeed;
    private readonly float _ikRotationSpeed;

    public Rotatable(Transform transform, MultiAimConstraint multiAimConstraint, float rotationSpeed, float ikRotationSpeed)
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
        if (direction.magnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        float angleDifference = Quaternion.Angle(_transform.rotation, targetRotation);

        // Если угол меньше пороговых значений, используем IK
        if (Mathf.Abs(angleDifference) < 65f)
        {
            _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, 1f, Time.deltaTime * _ikRotationSpeed);
        }
        else
        {
            // Иначе доворачиваем всей моделью
            _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, 0f, Time.deltaTime * _ikRotationSpeed);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}