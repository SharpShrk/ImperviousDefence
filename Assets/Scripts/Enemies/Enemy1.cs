using UnityEngine;
using Walls;

namespace Enemies
{
    public class Enemy1 : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private EnemyHealth _enemyHealth;
        private EnemyBehavior _enemyBehavior;
        private EnemyAnimator _enemyAnimator;

        public int Damage => _damage;

        private void Awake()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyBehavior = GetComponent<EnemyBehavior>();
            _enemyAnimator = GetComponent<EnemyAnimator>();
        }

        public void Initialize(int health, int damage)
        {
            _enemyHealth.Initialize(health);
            _damage = damage;
        }

        public void Activate()
        {
            _enemyBehavior.Activate();
        }

        public void AssignAttackPoint(WallAttackPoint point)
        {
            _enemyBehavior.AssignAttackPoint(point);
        }

        public void TakeDamage(int damage)
        {
            _enemyHealth.TakeDamage(damage);
        }

        private void Die()
        {
            _enemyAnimator.Die();
        }
    }
}
