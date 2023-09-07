using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeService : MonoBehaviour
{
    [SerializeField] private int _startCost;
    [SerializeField] private int _costForLevel;
    [SerializeField] private int _maxLevelUpgrade;
    [SerializeField] private TurretButtonUpgradeAttackSpeed _buttonUpgradeAttackSpeed;
    [SerializeField] private TurretButtonUpgradeDamage _buttonUpgradeDamage;
    [SerializeField] private Wallet _wallet;

    private TurretPresenter _currentTurretPresenter;

    public event UnityAction AttackSpeedUpgraded;
    public event UnityAction DamageUpgraded;

    private void OnEnable()
    {
        _buttonUpgradeAttackSpeed.ButtonUpgradeAttackSpeedPressed += TryUpdgradeAttackSpeed;
        _buttonUpgradeDamage.ButtonUpgradeDamagePressed += TryUpdgradeDamage;
    }

    private void OnDisable()
    {
        _buttonUpgradeAttackSpeed.ButtonUpgradeAttackSpeedPressed -= TryUpdgradeAttackSpeed;
        _buttonUpgradeDamage.ButtonUpgradeDamagePressed -= TryUpdgradeDamage;
    }

    public void SetCurrentTurretUpdates(TurretPresenter turretPresenter)
    {
        _currentTurretPresenter = turretPresenter;
    }

    private void TryUpdgradeDamage()
    {
        if (_currentTurretPresenter.GetLevelDamage() >= _maxLevelUpgrade)
        {
            //кинуть ошибку
            return;
        }

        int cost = GetCostUpgrade(_currentTurretPresenter.GetLevelDamage());
        bool success = _wallet.SpendMoney(cost);

        if (success)
        {
            _currentTurretPresenter.UpgradeDamage();
            DamageUpgraded?.Invoke();
        }
        else
        {
            Debug.Log("Не хватило денюшков"); //вывести в UI
        }   
    }

    private void TryUpdgradeAttackSpeed()
    {
        if (_currentTurretPresenter.GetLevelAttackSpeed() >= _maxLevelUpgrade)
        {
            //кинуть ошибку или сделать кнопку неактивной
            return;
        }

        int cost = GetCostUpgrade(_currentTurretPresenter.GetLevelAttackSpeed());
        bool success = _wallet.SpendMoney(cost);

        if (success)
        {
            _currentTurretPresenter.UpgradeAttackSpeed();
            AttackSpeedUpgraded?.Invoke();
        }
        else
        {
            Debug.Log("Не хватило денюшков"); //вывести в UI
        } 
    }

    public int GetCostUpgrade(int level)
    {
        int cost = _startCost + _costForLevel * level;
        return cost;
    }
}
