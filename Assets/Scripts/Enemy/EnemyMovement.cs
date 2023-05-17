using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private float _checkRadius = 1.5f;
    [SerializeField] private LayerMask _obstacleLayer;

    private Wall _targetWall;
    private bool _isMoving = true;
    private Rigidbody _rigidbody;
    private bool _isBlocked = false;
    private float _timeUntilCheck = 0.5f;
    private Transform _targetAttackPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void SetTargetWall(Wall targetWall)
    {
        _targetWall = targetWall;
        _targetAttackPoint = GetNearestAttackPoint(targetWall);
    }

    public void StartMoving()
    {
        _isMoving = true;
        _isBlocked = false;
        _rigidbody.isKinematic = false;
        SetTargetWall(_targetWall);
    }

    public void StopMoving()
    {
        _isMoving = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        _timeUntilCheck -= Time.fixedDeltaTime;

        if (_timeUntilCheck <= 0)
        {
            CheckForObstacles();
            _timeUntilCheck = 0.5f;
        }

        if (_isMoving && _targetWall != null && _isBlocked == false)
        {
            MoveTowardsTargetWall();
        }
    }

    private void CheckForObstacles()
    {
        _isBlocked = Physics.CheckSphere(transform.position, _checkRadius, _obstacleLayer);
    }

    private void MoveTowardsTargetWall()
    {
        if (_targetAttackPoint == null) return;

        Vector3 direction = (_targetAttackPoint.position - transform.position).normalized;
        _rigidbody.velocity = direction * _moveSpeed;

        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    private Transform GetNearestAttackPoint(Wall wall)
    {
        Transform nearestPoint = null;
        float minDistance = Mathf.Infinity;

        foreach (var point in wall.AttackPoints)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            if (distance < minDistance)
            {
                nearestPoint = point;
                minDistance = distance;
            }
        }

        return nearestPoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>() != null)
        {
            StopMoving();
        }
    }
}
