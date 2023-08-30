using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private Turret _turret;
    [SerializeField] private TurretAttackSpeed _turretAttackSpeed;
    [SerializeField] private TurretDamage _turretDamage;
    [SerializeField] private ColorChangeButtonUpgrade _colorChanger;
    [SerializeField] private TurretUpgradeView _upgradeMenu;
    [SerializeField] private TurretFactory _turretFactory;

    private void OnEnable()
    {
        _colorChanger.ButtonFullPressed += OpenShop;
    }

    private void OnDisable()
    {
        _colorChanger.ButtonFullPressed -= OpenShop;
    }

    //��������� � ������ ������� ������ ��������� � ����������� �� ���
    //�������� � �� � ���, ����� ������ �������, ����� � ��� ������� ������ ���������
    //�������� �� �� ���������� � ���, ��� �� ���������� �������
    //������� ������ � ���, ��� ��� ��������

    private void OpenShop() //�������������
    {
        if(_turret.IsPlaced)
        {           
            _upgradeMenu.OpenUpgradeTurretMenu(this);
        }
        else
        {
            _upgradeMenu.OpenBuyTurretMenu(this);
        }
    }

    public void TurretPlace()
    {
        if (_turret.IsPlaced == true)
            return;

        Debug.Log("������ �������, ������, �� ���������.");
        _turret.PlaceTurret();        
    }
}
