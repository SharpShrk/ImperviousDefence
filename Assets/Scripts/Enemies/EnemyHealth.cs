using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyHealthBar _enemyHealthBar;
        [SerializeField] private float _deathDuration;

        private int _health;
        private int _maxHealth;
        private bool _isDied;

        public event Action<int, int, EnemyHealth> OnEnemyDied;
        public event Action OnEnemyDiedForAttackPoint;

        public int Health => _health;

        public void Initialize(int health)
        {
            _health = health;
            _maxHealth = health;
            _isDied = false;
            gameObject.GetComponent<Collider>().enabled = true;
        }

        public void TakeDamage(int damage)
        {
            if (_isDied == false)
            {
                _health -= damage;
                _enemyHealthBar.UpdateHealthBar(_health, _maxHealth);

                if (_health <= 0)
                {
                    _isDied = true;

                    OnEnemyDied?.Invoke(0, 0, this);
                    OnEnemyDiedForAttackPoint?.Invoke();

                    gameObject.GetComponent<Collider>().enabled = false;

                    StartCoroutine(WaitForDieAnimationEnd());
                }
            }
        }

        public void HideHealthBar()
        {
            _enemyHealthBar.HideHealthBar();
        }

        private IEnumerator WaitForDieAnimationEnd()
        {
            yield return new WaitForSeconds(_deathDuration);

            gameObject.SetActive(false);
        }
    }
}
