using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackDelay = 1.5f;
    [SerializeField] private float _attackRange = 1.5f;

    private Wall _targetWall;
    private Enemy _enemy;
    private Coroutine _attackCoroutine;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    public void SetTargetWall(Wall targetWall)
    {
        _targetWall = targetWall;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = StartCoroutine(CheckAttackRange());
    }

    private IEnumerator CheckAttackRange()
    {
        while (true) //заменить тру
        {
            if (_targetWall != null)
            {
                float distance = Vector3.Distance(transform.position, _targetWall.transform.position);

                if (distance <= _attackRange)
                {
                    if (_attackCoroutine != null)
                    {
                        StopCoroutine(_attackCoroutine);
                    }

                    _attackCoroutine = StartCoroutine(AttackWall(_targetWall, _enemy));
                    GetComponent<EnemyMovement>().StopMoving();
                    break;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator AttackWall(Wall wall, Enemy enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            wall.TakeDamage(enemy.Damage);
        }
    }
}
