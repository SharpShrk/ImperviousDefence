using Lean.Localization;
using TurretsUI;
using UnityEngine;
using UnityEngine.Events;
using UserInterface;
using WalletAndScore;

namespace TurretScripts
{
    public class UpgradeService : MonoBehaviour
    {
        private const string MessageTurretLevelMax = "Turret_level_max";
        private const string MessageNotEnoughMoney = "Not_enough_money_to_upgrade";

        [SerializeField] private int _startCost;
        [SerializeField] private int _costForLevel;
        [SerializeField] private int _maxLevelUpgrade;
        [SerializeField] private TurretButtonUpgradeAttackSpeed _buttonUpgradeAttackSpeed;
        [SerializeField] private TurretButtonUpgradeDamage _buttonUpgradeDamage;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private InfoMessagePanel _infoMessagePanel;

        private TurretPresenter _currentTurretPresenter;

        public event UnityAction AttackSpeedUpgraded;

        public event UnityAction DamageUpgraded;

        private void OnEnable()
        {
            _buttonUpgradeAttackSpeed.ButtonUpgradePressed += OnTryUpdgradeAttackSpeed;
            _buttonUpgradeDamage.ButtonUpgradePressed += OnTryUpdgradeDamage;
        }

        private void OnDisable()
        {
            _buttonUpgradeAttackSpeed.ButtonUpgradePressed -= OnTryUpdgradeAttackSpeed;
            _buttonUpgradeDamage.ButtonUpgradePressed -= OnTryUpdgradeDamage;
        }

        public void SetCurrentTurretUpdates(TurretPresenter turretPresenter)
        {
            _currentTurretPresenter = turretPresenter;
        }

        public int GetCostUpgrade(int level)
        {
            int cost = _startCost + (_costForLevel * level);
            return cost;
        }

        private void OnTryUpdgradeDamage()
        {
            if (_currentTurretPresenter.GetLevelDamage() >= _maxLevelUpgrade)
            {
                string message = LeanLocalization.GetTranslationText(MessageTurretLevelMax);

                _infoMessagePanel.OpenMessagePanel(message);

                return;
            }

            int cost = GetCostUpgrade(_currentTurretPresenter.GetLevelDamage());
            bool success = _wallet.SpendMoney(cost);

            if (success)
            {
                _currentTurretPresenter.UpgradeDamage();
                DamageUpgraded?.Invoke();
            }
            else
            {
                string message = LeanLocalization.GetTranslationText(MessageNotEnoughMoney);
                _infoMessagePanel.OpenMessagePanel(message);
            }
        }

        private void OnTryUpdgradeAttackSpeed()
        {
            if (_currentTurretPresenter.GetLevelAttackSpeed() >= _maxLevelUpgrade)
            {
                string message = LeanLocalization.GetTranslationText(MessageTurretLevelMax);
                _infoMessagePanel.OpenMessagePanel(message);

                return;
            }

            int cost = GetCostUpgrade(_currentTurretPresenter.GetLevelAttackSpeed());
            bool success = _wallet.SpendMoney(cost);

            if (success)
            {
                _currentTurretPresenter.UpgradeAttackSpeed();
                AttackSpeedUpgraded?.Invoke();
            }
            else
            {
                string message = LeanLocalization.GetTranslationText(MessageNotEnoughMoney);
                _infoMessagePanel.OpenMessagePanel(message);
            }
        }
    }
}