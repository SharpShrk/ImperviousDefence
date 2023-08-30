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
    [SerializeField] private PlaceTurretView _placeTurretView;
    [SerializeField] private TurretFactory _turretFactory;

    private void OnEnable()
    {
        _colorChanger.ButtonFullPressed += OpenShop;
        //_placeTurretView.TurretBuyed += InitTurretPlace; //���������� ����� ������ ����������
    }

    //��������� � ������ ������� ������ ��������� � ����������� �� ���
    //�������� � �� � ���, ����� ������ �������, ����� � ��� ������� ������ ���������
    //�������� �� �� ���������� � ���, ��� �� ���������� �������
    //������� ������ � ���, ��� ��� ��������

    private void OpenShop() //�������������
    {
        if(_turret.IsPlaced)
        {
            //�������� ���� ������� ������, ������ ���������� ���� ������
        }
        else
        {
            //������� ���� � �������� ������? ��� ������ ��� �� ������?
            _upgradeMenu.OpenBuyTurretMenu();
        }
    }

    private void InitTurretPlace()
    {

    }
}
