using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;

    private Wall _targetWall;
    private bool _isMoving = true;

    public void SetTargetWall(Wall targetWall)
    {
        _targetWall = targetWall;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            MoveTowardsTargetWall();
        }
    }

    private void MoveTowardsTargetWall()
    {
        if (_targetWall != null)
        {
            transform.LookAt(_targetWall.transform);
            transform.position += transform.forward * _moveSpeed * Time.fixedDeltaTime;
        }
    }
}
