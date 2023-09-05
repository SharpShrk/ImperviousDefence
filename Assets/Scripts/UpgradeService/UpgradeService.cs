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

    private void TryUpdgradeDamage() //����������� �� ������ ��������
    {
        //������� ������, ���� ������, �� ����������

        if (_currentTurretPresenter.GetLevelDamage() >= _maxLevelUpgrade)
        {
            //������ ������
            return;
        }

        UpgradeDamage();
    }

    private void TryUpdgradeAttackSpeed() //����������� �� ������ ��������
    {
        //������� ������, ���� ������, �� ����������

        if (_currentTurretPresenter.GetLevelAttackSpeed() >= _maxLevelUpgrade)
        {
            //������ ������
            return;
        }

        UpgradeAttackSpeed();
    }

    public int GetCostUpgrade(int level) //��� ����� ���� �������� ����� ����
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
