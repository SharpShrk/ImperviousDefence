using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private const float MovementThreshold = 0.1f;
        private const float ForwardAngleThreshold = 45f;
        private const float BackwardAngleThreshold = 135f;
        private const float DirectionThreshold = 0f;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private MultiAimConstraint _multiAimConstraint;
        [SerializeField] private float _ikRotationSpeed;

        private IMovable _movable;
        private IRotatable _rotatable;
        private PlayerInputHandler _inputHandler;
        private Animator _animator;
        private GameOverChecker _gameOverChecker;

        private void Awake()
        {
            EnableMultiAimConstrain();
            _animator = GetComponent<Animator>();
            _movable = new Movable(transform, _moveSpeed);
            _rotatable = new Rotatable(transform, _multiAimConstraint, _rotationSpeed, _ikRotationSpeed);
            _inputHandler = new PlayerInputHandler(_mainCamera);
            _gameOverChecker = FindObjectOfType<GameOverChecker>();
        }

        private void OnEnable()
        {
            _inputHandler.Enable();
            _gameOverChecker.GameOvered += OnDisableMultiAimConstrain;
        }

        private void OnDisable()
        {
            _inputHandler.Disable();
            _gameOverChecker.GameOvered -= OnDisableMultiAimConstrain;
        }

        private void FixedUpdate()
        {
            if (Time.timeScale < 1)
            {
                OnDisableMultiAimConstrain();
            }
            else
            {
                EnableMultiAimConstrain();
            }

            Vector3 direction = _inputHandler.GetMoveDirection();
            Vector3 lookDirection = _inputHandler.GetLookDirection(transform.position);

            _movable.Move(direction);
            _rotatable.Rotate(_inputHandler.GetMouseWorldPosition());

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

            if (direction.magnitude > MovementThreshold)
            {
                if (angle < ForwardAngleThreshold)
                {
                    _animator.SetBool("isRunningForward", true);
                }
                else if (angle > BackwardAngleThreshold)
                {
                    _animator.SetBool("isRunningBackward", true);
                }
                else
                {
                    if (crossProduct > DirectionThreshold)
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

        private void EnableMultiAimConstrain()
        {
            float enableWeight = 1f;
            ChangeConstraintWeight(enableWeight);
        }

        private void OnDisableMultiAimConstrain()
        {
            float disableWeight = 0f;
            ChangeConstraintWeight(disableWeight);
        }

        private void ChangeConstraintWeight(float newWeight)
        {
            int sourceIndex = 0;
            var sourceObjects = _multiAimConstraint.data.sourceObjects;

            var weightedTransform = sourceObjects[sourceIndex];
            weightedTransform.weight = newWeight;

            sourceObjects[sourceIndex] = weightedTransform;
            _multiAimConstraint.data.sourceObjects = sourceObjects;
        }
    }
}