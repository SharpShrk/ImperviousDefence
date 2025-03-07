using System;
using UnityEngine;
using Walls;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyMovement : MonoBehaviour
    {
        private const string AnimatorTriggerRunning = "isRunning";

        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private LayerMask _obstacleLayer;

        private bool _isMoving = false;
        private Rigidbody _rigidbody;
        private Transform _targetAttackPoint;
        private EnemyAttack _enemyAttack;
        private Animator _animator;
        private bool _isEnougthAttackPoint = false;

        public event Action MovementStopped;

        public Transform TargetAttackPoint => _targetAttackPoint;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _animator = GetComponent<Animator>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _animator.SetBool(AnimatorTriggerRunning, true);
        }

        private void FixedUpdate()
        {
            if (_isMoving && _isEnougthAttackPoint)
            {
                MoveTowardsTargetWall();
            }
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
            _animator.SetBool(AnimatorTriggerRunning, false);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
        }

        private void MoveTowardsTargetWall()
        {
            if (_targetAttackPoint == null)
            {
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
                MovementStopped?.Invoke();
            }
        }
    }
}