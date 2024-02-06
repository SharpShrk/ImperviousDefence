using System;
using UnityEngine;

namespace WalletAndScore
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private int _startMoney;
        [SerializeField] private CoinsSaver _coinsSaver;

        private int _money;

        public event Action<int> OnMoneyChanged;

        public int Money => _money;

        public void SetStartValue(int value)
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }

        public void AddMoney(int amount)
        {
            _money += amount;
            OnMoneyChanged?.Invoke(_money);
            _coinsSaver.OnSaveCoins();
        }

        public bool SpendMoney(int amount)
        {
            if (_money < amount)
            {
                return false;
            }

            _money -= amount;
            OnMoneyChanged?.Invoke(_money);
            _coinsSaver.OnSaveCoins();

            return true;
        }
    }
}