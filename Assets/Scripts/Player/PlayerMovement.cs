using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
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
        _gameOverChecker.GameOverEvent += DisableMultiAimConstrain;
    }

    private void OnDisable()
    {
        _inputHandler.Disable();
        _gameOverChecker.GameOverEvent -= DisableMultiAimConstrain;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale < 1)
        {
            DisableMultiAimConstrain();
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

    private void EnableMultiAimConstrain()
    {
        float enableWeight = 1f;
        ChangeConstraintWeight(enableWeight);
    }

    private void DisableMultiAimConstrain()
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