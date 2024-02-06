using TMPro;
using UnityEngine;
using WalletAndScore;

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
