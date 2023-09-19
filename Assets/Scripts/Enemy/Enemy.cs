using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _rewardMoney;
    [SerializeField] private int _rewardScore;

    private int _health;
    private int _damage;
    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;

    public event Action<int, int, Enemy> OnEnemyDied;
    public event Action OnEnemyDiedForAttackPoint;

    public int Health => _health;
    public int Damage => _damage;

    private void Awake() //тут был эвейк, возможно что то сломается
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    public void Initialize(int health, int damage)
    {
        _health = health;
        _damage = damage;
    }

    public void Activate()
    {
        Wall targetWall = FindClosestWall();
        //_enemyMovement.SetupAttackPoint(targetWall);
        _enemyAttack.SetTargetWall(targetWall);
    }

    public void AssignAttackPoint(WallAttackPoint point)
    {
        _enemyMovement.SetupAttackPoint(point);
        _enemyAttack.SetTargetWall(point.GetComponentInParent<Wall>());
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            OnEnemyDied?.Invoke(_rewardMoney, _rewardScore, this);
            OnEnemyDiedForAttackPoint?.Invoke();
            Die();
        }
    }

    private void Die()
    {        
        gameObject.SetActive(false);
    }

    private Wall FindClosestWall()
    {
        Wall[] walls = FindObjectsOfType<Wall>();
        print(walls.Length);
        Wall closestWall = null;
        float minDistance = float.MaxValue;

        foreach (Wall wall in walls)
        {
            float distance = Vector3.Distance(transform.position, wall.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestWall = wall;
            }
        }

        return closestWall;
    }
}
