using UnityEngine;
using UnityEngine.Events;

namespace Wave
{
    public class Waves : MonoBehaviour
    {
        private const int WaveHealthIncrease = 3;
        private const int WaveAttackIncrease = 6;
        private const int WaveEnemyCountIncrease = 5;

        [SerializeField] private int _initialHealth = 100;
        [SerializeField] private int _initialAttack = 1;
        [SerializeField] private int _initialEnemyCount = 4;
        [SerializeField] private int _healthIncrease = 5;
        [SerializeField] private int _attackIncrease = 1;
        [SerializeField] private int _countIncrease = 1;

        private int _currentHealth;
        private int _currentAttack;
        private int _currentEnemyCount;

        public event UnityAction<int> WaveChanged;

        public int CurrentWave { get; private set; } = 0;

        private void Awake()
        {
            Time.timeScale = 1;

            _currentHealth = _initialHealth;
            _currentAttack = _initialAttack;
            _currentEnemyCount = _initialEnemyCount;
        }

        public void AdvanceToNextWave()
        {
            CurrentWave++;
            WaveChanged?.Invoke(CurrentWave);

            if (CurrentWave % WaveHealthIncrease == 0)
            {
                _currentHealth += _healthIncrease;
            }

            if (CurrentWave % WaveAttackIncrease == 0)
            {
                _currentAttack += _attackIncrease;
            }

            if (CurrentWave % WaveEnemyCountIncrease == 0)
            {
                _currentEnemyCount += _countIncrease;
            }
        }

        public int GetEnemyHealth()
        {
            return _currentHealth;
        }

        public int GetEnemyAttack()
        {
            return _currentAttack;
        }

        public int GetEnemyCount()
        {
            return _currentEnemyCount;
        }
    }
}