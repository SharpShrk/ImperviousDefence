using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MobilePlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private MultiAimConstraint _multiAimConstraint;
    [SerializeField] private float _ikRotationSpeed;
    [SerializeField] private LeftJoystick _moveJoystick;
    [SerializeField] private RightJoystick _lookJoystick;

    private IMovable _movable;
    private IRotatable _rotatable;
    private MobileInputHandler _inputHandler;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movable = new Movable(transform, _moveSpeed);
        _rotatable = new Rotatable(transform, _multiAimConstraint, _rotationSpeed, _ikRotationSpeed);
        _inputHandler = new MobileInputHandler(_moveJoystick, _lookJoystick);
    }

    private void FixedUpdate()
    {
        Vector3 direction = _inputHandler.GetMoveDirection();
        Vector3 lookDirection = _inputHandler.GetLookDirection(transform.position);

        _movable.Move(direction);
        _rotatable.Rotate(lookDirection);

        SetRunningAnimations(direction, lookDirection);
    }

    private void SetRunningAnimations(Vector3 direction, Vector3 lookDirection)
    {
        _animator.SetBool("isRunningForward", false);
        _animator.SetBool("isRunningBackward", false);
        _animator.SetBool("isRunningRight", false);
        _animator.SetBool("isRunningLeft", false);

        float angle = Vector3.Angle(direction, lookDirection);
        float crossProduct = Vector3.Cross(direction, lookDirection).y;

        if (direction.magnitude > 0.1f)
        {
            if (angle < 45)
            {
                _animator.SetBool("isRunningForward", true);
            }
            else if (angle > 135)
            {
                _animator.SetBool("isRunningBackward", true);
            }
            else
            {
                if (crossProduct > 0)
                {
                    _animator.SetBool("isRunningRight", true);
                }
                else
                {
                    _animator.SetBool("isRunningLeft", true);
                }
            }
        }
    }
}
