using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletText;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.OnMoneyChanged += SetValue;
    }

    private void OnDisable()
    {
        _wallet.OnMoneyChanged -= SetValue;
    }

    private void SetValue(int value)
    {
        _walletText.text = value.ToString();
    }
}