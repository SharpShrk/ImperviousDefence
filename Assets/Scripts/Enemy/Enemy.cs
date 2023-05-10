using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health;
    private int _damage;

    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;

    public int Health => _health;
    public int Damage => _damage;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }

    public void Initialize(int health, int damage)
    {
        _health = health;
        _damage = damage;
    }

    private void OnEnable()
    {
        Wall targetWall = FindClosestWall();
        _enemyMovement.SetTargetWall(targetWall);
        _enemyAttack.SetTargetWall(targetWall);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
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
