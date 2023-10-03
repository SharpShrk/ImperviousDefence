using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int _rewardMoney;
    [SerializeField] private int _rewardScore;
    [SerializeField] private EnemyHealthBar _enemyHealthBar;
    [SerializeField] private float _deathDuration;

    private int _health;
    private int _maxHealth;
    private int _damage;
    private bool _isDied;
    private EnemyMovement _enemyMovement;
    private EnemyAttack _enemyAttack;
    private Animator _animator;

    public event Action<int, int, Enemy> OnEnemyDied;
    public event Action OnEnemyDiedForAttackPoint;

    public int Health => _health;
    public int Damage => _damage;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _animator = GetComponent<Animator>();
    }

    public void Initialize(int health, int damage)
    {
        _health = health;
        _maxHealth = health;
        _damage = damage;
        _isDied = false;
    }

    public void Activate()
    {
        Wall targetWall = FindClosestWall();
        _enemyAttack.SetTargetWall(targetWall);       
    }

    public void HideHealthBar()
    {
        _enemyHealthBar.HideHealthBar();
    }

    public void AssignAttackPoint(WallAttackPoint point)
    {
        _enemyMovement.SetupAttackPoint(point);
        _enemyAttack.SetTargetWall(point.GetComponentInParent<Wall>());
    }

    public void TakeDamage(int damage)
    {
        if(_isDied == false)
        {
            _health -= damage;
            _enemyHealthBar.UpdateHealthBar(_health, _maxHealth);

            if (_health <= 0)
            {
                _isDied = true;

                _enemyMovement.StopMoving();
                _enemyHealthBar.HideHealthBar();

                OnEnemyDied?.Invoke(_rewardMoney, _rewardScore, this);
                OnEnemyDiedForAttackPoint?.Invoke();

                _animator.SetTrigger("isDied");
                StartCoroutine(WaitForDieAnimationEnd());
            }
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

    private IEnumerator WaitForDieAnimationEnd()
    {
        yield return new WaitForSeconds(_deathDuration);

        Die();
    }
}
