using UnityEngine;

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