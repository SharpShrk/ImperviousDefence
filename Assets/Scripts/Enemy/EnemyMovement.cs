using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private LayerMask _obstacleLayer;

    private bool _isMoving = false;
    private Rigidbody _rigidbody;
    private Transform _targetAttackPoint;
    private EnemyAttack _enemyAttack;
    private Animator _animator;
    private bool _isEnougthAttackPoint = false;

    public Transform TargetAttackPoint => _targetAttackPoint;
    public bool IsMoving => _isMoving;

    public event Action MovementStoppedAction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _animator.SetBool("isRunning", true);
    }

    public void SetupAttackPoint(WallAttackPoint target)
    {
        _targetAttackPoint = target.transform;

        if (_isEnougthAttackPoint == false)
        {
            target.SetOccupied(gameObject.GetComponent<Enemy>());
            _isEnougthAttackPoint = true;
        }

        StartMoving();
    }

    public void StartMoving()
    {
        _isMoving = true;
        _rigidbody.isKinematic = false;
    }

    public void StopMoving()
    {
        _isMoving = false;
        _animator.SetBool("isRunning", false);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (_isMoving && _isEnougthAttackPoint)
        {
            MoveTowardsTargetWall();
        }
    }

    private void MoveTowardsTargetWall()
    {
        if (_targetAttackPoint == null)
        {
            Debug.LogError("Target attack point is null for " + gameObject.name);
            return;
        }

        Vector3 direction = (_targetAttackPoint.position - transform.position).normalized;
        _rigidbody.velocity = direction * _moveSpeed;

        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        transform.rotation = Quaternion.LookRotation(lookDirection);

        float distance = Vector3.Distance(transform.position, _targetAttackPoint.transform.position);

        if (distance <= _enemyAttack.AttackRange)
        {
            StopMoving();
            MovementStoppedAction?.Invoke();
        }
    }

    /*private Transform GetNearestAttackPoint(Wall wall)
    {
        Transform nearestPoint = null;
        float minDistance = Mathf.Infinity;


        foreach (var point in wall.AttackPoints)
        {
            WallAttackPoint attackPoint = point.GetComponent<WallAttackPoint>();

            if (attackPoint != null && attackPoint.IsOccupied == false)
            {
                float distance = Vector3.Distance(transform.position, point.position);

                if (distance < minDistance)
                {
                    nearestPoint = point;
                    minDistance = distance;
                }
            }
        }

        return nearestPoint;
    }*/
}
