using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _attackDelay = 1.5f;
    [SerializeField] private float _attackRange = 1.5f;

    public float AttackRange => _attackRange;

    private Wall _targetWall;
    private Enemy _enemy;
    private EnemyMovement _movement;
    private Coroutine _checkAttackRangeCoroutine;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _movement = GetComponent<EnemyMovement>();
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
        _targetWall = targetWall;
    }

    private IEnumerator AttackWall(Wall wall, Enemy enemy)
    {
        while (true)
        {
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

        _attackCoroutine = StartCoroutine(AttackWall(_targetWall, _enemy));
    }

    private void StopCheckAttackRange() //не используется пока
    {
        if (_checkAttackRangeCoroutine != null)
        {
            StopCoroutine(_checkAttackRangeCoroutine);
        }
    }
}
