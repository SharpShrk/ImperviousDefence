using System.Collections;
using UnityEngine;
using Walls;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]

    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _attackDelay = 1.5f;
        [SerializeField] private float _attackRange = 1.5f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _punchSounds;

        private Wall _targetWall;
        private Enemy _enemy;
        private EnemyMovement _movement;
        private Coroutine _attackCoroutine;
        private Animator _animator;

        public float AttackRange => _attackRange;

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _movement = GetComponent<EnemyMovement>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _movement.MovementStoppedAction += OnStartAttackCoroutine;
        }

        private void OnDisable()
        {
            _movement.MovementStoppedAction -= OnStartAttackCoroutine;
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
            while (_enemy.enabled)
            {
                _animator.SetTrigger("attack");
                yield return new WaitForSeconds(_attackDelay);

                wall.TakeDamage(enemy.Damage);
            }
        }

        private void OnStartAttackCoroutine()
        {
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
    }
}