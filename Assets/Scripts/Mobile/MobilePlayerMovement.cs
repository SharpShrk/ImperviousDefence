using PlayerScripts;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Mobile
{
    public class MobilePlayerMovement : MonoBehaviour
    {
        private const string AnimatorTriggerRunningForward = "isRunningForward";
        private const string AnimatorTriggerRunningBackward = "isRunningBackward";
        private const string AnimatorTriggerRunningRight = "isRunningRight";
        private const string AnimatorTriggerRunningLeft = "isRunningLeft";
        private const float MovementThreshold = 0.1f;
        private const float ForwardAngleThreshold = 45f;
        private const float BackwardAngleThreshold = 135f;
        private const float DirectionThreshold = 0f;

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
            _animator.SetBool(AnimatorTriggerRunningForward, false);
            _animator.SetBool(AnimatorTriggerRunningBackward, false);
            _animator.SetBool(AnimatorTriggerRunningRight, false);
            _animator.SetBool(AnimatorTriggerRunningLeft, false);

            float angle = Vector3.Angle(direction, lookDirection);
            float crossProduct = Vector3.Cross(direction, lookDirection).y;

            if (direction.magnitude > MovementThreshold)
            {
                if (angle < ForwardAngleThreshold)
                {
                    _animator.SetBool(AnimatorTriggerRunningForward, true);
                }
                else if (angle > BackwardAngleThreshold)
                {
                    _animator.SetBool(AnimatorTriggerRunningBackward, true);
                }
                else
                {
                    if (crossProduct > DirectionThreshold)
                    {
                        _animator.SetBool(AnimatorTriggerRunningRight, true);
                    }
                    else
                    {
                        _animator.SetBool(AnimatorTriggerRunningLeft, true);
                    }
                }
            }
        }
    }
}