using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> OnMoneyChanged;

    private int _money;

    public int Money => _money;

    private void Start()
    {
        _money = 0;
        OnMoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int amount) //rewardCollector ������ ���� ��������
    {
        _money += amount;
        OnMoneyChanged?.Invoke(_money);
    }

    public bool SpendMoney(int amount) //�������� ����� �� ��� �������� ����� ���? �������� � ������, ���� ��
    {
        if (_money < amount)
        {
            return false;  // ������������ �������
        }

        _money -= amount;
        OnMoneyChanged?.Invoke(_money);
        return true;  // �������� �������
    }
}
