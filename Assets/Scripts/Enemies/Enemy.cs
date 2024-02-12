using System;
using UnityEngine;
using Walls;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private EnemyHealth _enemyHealth;
        private EnemyBehavior _enemyBehavior;
        private EnemyAnimator _enemyAnimator;

        public event Action OnEnemyDiedForAttackPoint;

        public int Damage => _damage;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyBehavior = GetComponent<EnemyBehavior>();
            _enemyAnimator = GetComponent<EnemyAnimator>();

            
        }

        private void OnEnable()
        {
            _enemyHealth.OnEnemyDie += Die;
        }

        private void OnDisable()
        {
            _enemyHealth.OnEnemyDie -= Die;
        }

        public void Initialize(int health, int damage)
        {
            _enemyHealth.Initialize(health);
            _damage = damage;
        }

        public void AssignAttackPoint(WallAttackPoint point)
        {
            _enemyBehavior.AssignAttackPoint(point);
        }

        private void Die()
        {
            OnEnemyDiedForAttackPoint?.Invoke();

            _enemyAnimator.Die();
        }
    }
}
