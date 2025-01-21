using System.Collections;
using UnityEngine;
using Walls;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]

    public class EnemyAttack : MonoBehaviour
    {
        private const string AttackAnimatorTrigger = "attack";

        [SerializeField] private float _attackDelay = 1.5f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _punchSounds;

        private Wall _targetWall;
        private Enemy _enemy;
        private EnemyHealth _health;
        private EnemyMovement _movement;
        private Coroutine _attackCoroutine;
        private Animator _animator;
        private bool _isAttack;
        private WaitForSeconds _waitAttackDelay;

        public float AttackRange => _attackRange;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _health = GetComponent<EnemyHealth>();
            _movement = GetComponent<EnemyMovement>();
            _animator = GetComponent<Animator>();
            _waitAttackDelay = new WaitForSeconds(_attackDelay);
        }

        private void OnEnable()
        {
            _movement.MovementStopped += OnTryStartWallAttack;
            _health.EnemyDyingNoParams += OnStopAttack;
        }

        private void OnDisable()
        {
            _movement.MovementStopped -= OnTryStartWallAttack;
            _health.EnemyDyingNoParams -= OnStopAttack;
        }

        public void SetTargetWall(Wall targetWall)
        {
            if (targetWall == null)
            {
                return;
            }

            _targetWall = targetWall;
        }

        public void PlayRandomPunchSound()
        {
            AudioClip sound;
            sound = _punchSounds[Random.Range(0, _punchSounds.Length)];
            _audioSource.PlayOneShot(sound);
        }

        private IEnumerator AttackWall(Wall wall, Enemy enemy)
        {
            while (_isAttack)
            {
                _animator.SetTrigger(AttackAnimatorTrigger);
                PlayRandomPunchSound();
                yield return _waitAttackDelay;

                wall.TakeDamage(enemy.Damage);
            }
        }

        private void OnTryStartWallAttack()
        {
            _isAttack = true;

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }

            WallAttackPoint attackPoint = _movement.TargetAttackPoint.GetComponent<WallAttackPoint>();

            if (attackPoint != null)
            {
                _attackCoroutine = StartCoroutine(AttackWall(_targetWall, _enemy));
            }
        }

        private void OnStopAttack()
        {
            _isAttack = false;

            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }
    }
}