using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class TurretUpgrade : MonoBehaviour
{    
    [SerializeField] protected int StartCostValue;
    [SerializeField] protected int StartLevelValue;

    protected int LevelValue;
    protected int CostValue;

    //����������. ������ �����. � ������ ������ �������� ������ �� �� ��������.
    //�������� ����� �������. ������� �������. ��������� �������. ��� �������� ���� ������� ���������� ������ � ������ ���� ������ � ������������ � UI.
    //���������� � ������.

    protected void OnEnable()
    {
        LevelValue = StartLevelValue;
        CostValue = StartCostValue;       
    }

    protected abstract void OnButtonClick();

    public void SetCostValue(int value) //������� ���������, �������� ������ �� �������� �� �������
    {
        CostValue = value; //�������� �������� � UI
    }

    public void SetLevelValue() //������� ���������, �������� ������ �� �������� �� �������
    {
        LevelValue++; //�������� �������� � UI
    }
}
