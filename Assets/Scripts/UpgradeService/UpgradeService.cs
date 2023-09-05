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

    private void TryUpdgradeDamage() //подписаться на кнопку апгрейда
    {
        //списать деньги, если удачно, то апдейтимся

        if (_currentTurretPresenter.GetLevelDamage() >= _maxLevelUpgrade)
        {
            //кинуть ошибку
            return;
        }

        UpgradeDamage();
    }

    private void TryUpdgradeAttackSpeed() //подписаться на кнопку апгрейда
    {
        //списать деньги, если удачно, то апдейтимся

        if (_currentTurretPresenter.GetLevelAttackSpeed() >= _maxLevelUpgrade)
        {
            //кинуть ошибку
            return;
        }

        UpgradeAttackSpeed();
    }

    public int GetCostUpgrade(int level) //эта хуйня явно работает через жопу
    {
        int cost = _startCost + _costForLevel * level;
        return cost;
    }

    private void UpgradeDamage()
    {
        _currentTurretPresenter.UpgradeDamage();
        DamageUpgraded?.Invoke();
    }

    private void UpgradeAttackSpeed()
    {
        _currentTurretPresenter.UpgradeAttackSpeed();
        AttackSpeedUpgraded?.Invoke();
    }
}
