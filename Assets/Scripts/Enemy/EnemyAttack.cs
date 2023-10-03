using System.Collections;
using UnityEngine;

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
        _movement.MovementStoppedAction += StartAttackCoroutine;
    }

    private void OnDisable()
    {
        _movement.MovementStoppedAction -= StartAttackCoroutine;    
    }

    public void SetTargetWall(Wall targetWall)
    {
        if (targetWall == null)
        {
            Debug.LogError("Target Wall is null");
            return;
        }

        _targetWall = targetWall;
    }

    public void PlayRandomPunchSound() //starts from the animator event
    {
        AudioClip sound;
        sound = _punchSounds[Random.Range(0, _punchSounds.Length)];
        _audioSource.PlayOneShot(sound);
    }

    private IEnumerator AttackWall(Wall wall, Enemy enemy)
    {
        while (true)
        {
            _animator.SetTrigger("attack");
            yield return new WaitForSeconds(_attackDelay);

            wall.TakeDamage(enemy.Damage);
        }
    }

    private void StartAttackCoroutine()
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
