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

    private void Awake()
    {
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

    private void Update()
    {
        Vector3 direction = _inputHandler.GetMoveDirection();
        _movable.Move(direction);
        _rotatable.Rotate(_inputHandler.GetMouseWorldPosition());
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
        _transform.position += direction * _moveSpeed * Time.deltaTime;
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