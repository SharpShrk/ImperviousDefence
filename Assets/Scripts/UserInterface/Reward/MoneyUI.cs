using UnityEngine;
using WalletAndScore;

namespace UIReward
{
    public class MoneyUI : RewardUI
    {
        [SerializeField] private Wallet _wallet;

        protected override void OnEnable()
        {
            _wallet.OnMoneyChanged += SetValue;
        }

        protected override void OnDisable()
        {
            _wallet.OnMoneyChanged -= SetValue;
        }
    }
}