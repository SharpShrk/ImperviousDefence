using UnityEngine;

public class MobileIKTargetMover : MonoBehaviour
{
    [SerializeField] private float _distanceFromPlayer = 2f;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private RightJoystick _rightJoystick;
    [SerializeField] private float _inputThreshold = 0.3f;

    private Vector3 _lastKnownDirection;

    void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 joystickInput = _rightJoystick.GetInputDirection();

        if (joystickInput.magnitude >= _inputThreshold)
        {
            _lastKnownDirection = joystickInput.normalized;
        }

        Vector3 targetDirection = _lastKnownDirection * _distanceFromPlayer;
        Vector3 targetPosition = _startPosition.position + targetDirection;

        targetPosition.y = _startPosition.position.y;
        transform.position = targetPosition;
    }
}
