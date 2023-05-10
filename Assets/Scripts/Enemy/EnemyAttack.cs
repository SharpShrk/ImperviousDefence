using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackDelay = 1.5f;
    [SerializeField] private float _attackRange = 1.5f;

    private Wall _targetWall;
    private Coroutine _attackCoroutine;

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
        while (true)
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
                    _attackCoroutine = StartCoroutine(AttackWall(_targetWall));
                    GetComponent<EnemyMovement>().StopMoving();
                    break;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator AttackWall(Wall wall)
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackDelay);
            //wall.TakeDamage(Enemy.Damage);
        }
    }
}
