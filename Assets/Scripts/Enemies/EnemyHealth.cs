using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyHealthBar _enemyHealthBar;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private float _deathDuration;

        private int _health;
        private int _maxHealth;
        private bool _isDied;
        private WaitForSeconds _deathWait;

        public event Action<int, int, EnemyHealth> OnEnemyDying;
        public event Action OnEnemyDyingNoParams;

        private void Awake()
        {
            _deathWait = new WaitForSeconds(_deathDuration);
        }

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
                    _enemyMovement.StopMoving();
                    _enemyHealthBar.HideHealthBar();

                    OnEnemyDying?.Invoke(0, 0, this);
                    OnEnemyDyingNoParams?.Invoke();

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
            yield return _deathWait;
            gameObject.SetActive(false);
        }
    }
}