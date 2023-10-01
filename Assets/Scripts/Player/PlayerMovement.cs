using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera mainCamera;

    private IMovable _movable;
    private IRotatable _rotatable;
    private PlayerInputHandler _inputHandler;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movable = new Movable(transform, moveSpeed);
        _rotatable = new Rotatable(transform, rotationSpeed);
        _inputHandler = new PlayerInputHandler(mainCamera);
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
        else
        {
            _animator.SetBool("isRunningForward", false);
            _animator.SetBool("isRunningBackward", false);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
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
    private readonly float _rotationSpeed;

    public Rotatable(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public void Rotate(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _transform.position).normalized;
        if (direction.magnitude < 0.1f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}