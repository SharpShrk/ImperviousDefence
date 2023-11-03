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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movable = new Movable(transform, _moveSpeed);
        _rotatable = new Rotatable(transform, _multiAimConstraint, _rotationSpeed, _ikRotationSpeed);
        _inputHandler = new PlayerInputHandler(_mainCamera);
    }

    private void OnEnable()
    {
        _inputHandler.Enable();
    }

    private void OnDisable()
    {
        _inputHandler.Disable();
    }

    private void FixedUpdate()
    {
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
}

public interface IMovable
{
    void Move(Vector3 direction);
}

public interface IRotatable
{
    void Rotate(Vector3 targetPosition);
}

public class Movable : IMovable
{
    private readonly Transform _transform;
    private readonly float _moveSpeed;

    public Movable(Transform transform, float moveSpeed)
    {
        _transform = transform;
        _moveSpeed = moveSpeed;
    }

    public void Move(Vector3 direction)
    {
        direction.Normalize();
        float originalY = _transform.position.y;
        _transform.position += direction * _moveSpeed * Time.deltaTime;
        _transform.position = new Vector3(_transform.position.x, originalY, _transform.position.z);
    }
}

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
        _multiAimConstraint.enabled = true;

        if (Time.timeScale < 1)
        {
            _multiAimConstraint.enabled = false;
            return;
        }


        Vector3 direction = (targetPosition - _transform.position).normalized;
        if (direction.magnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        float angleDifference = Quaternion.Angle(_transform.rotation, targetRotation);

        // ≈сли угол меньше пороговых значений, используем IK
        if (Mathf.Abs(angleDifference) < 65f)
        {
            _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, 1f, Time.deltaTime * _ikRotationSpeed);
        }
        else
        {
            // »наче доворачиваем всей моделью
            _multiAimConstraint.weight = Mathf.Lerp(_multiAimConstraint.weight, 0f, Time.deltaTime * _ikRotationSpeed);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}


