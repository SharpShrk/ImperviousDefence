using UnityEngine;

public class Waves : MonoBehaviour
{
    public int CurrentWave { get; private set; } = 1;

    [SerializeField] private int _initialHealth = 100;
    [SerializeField] private int _initialAttack = 1;
    [SerializeField] private int _initialEnemyCount = 4;
    [SerializeField] private int _healthIncrease = 5;
    [SerializeField] private int _attackIncrease = 1;
    [SerializeField] private int _countIncrease = 1;

    private int _currentHealth;
    private int _currentAttack;
    private int _currentEnemyCount;

    private void Awake()
    {
        _currentHealth = _initialHealth;
        _currentAttack = _initialAttack;
        _currentEnemyCount = _initialEnemyCount;
    }

    public void AdvanceToNextWave()
    {
        CurrentWave++;
    }

    public int GetEnemyHealth()
    {
        if (CurrentWave % 2 == 0)
        {
            return _currentHealth += _healthIncrease;
        }
        else
        {
            return _currentHealth;
        }
    }

    public int GetEnemyAttack()
    {
        if (CurrentWave % 4 == 0)
        {
            return _currentAttack += _initialAttack;
        }
        else
        {
            return _currentAttack;
        }
    }

    public int GetEnemyCount()
    {
        if (CurrentWave % 3 == 0)
        {
            return _currentEnemyCount += _initialEnemyCount;
        }
        else
        {
            return _currentEnemyCount;
        }
    }
}
