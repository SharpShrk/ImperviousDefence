using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using UnityEngine.Events;
using UserInterface;
using WalletAndScore;

namespace TurretScripts
{
    public class UpgradeService : MonoBehaviour
    {
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
            _buttonUpgradeAttackSpeed.ButtonUpgradeAttackSpeedPressed += OnTryUpdgradeAttackSpeed;
            _buttonUpgradeDamage.ButtonUpgradeDamagePressed += OnTryUpdgradeDamage;
        }

        private void OnDisable()
        {
            _buttonUpgradeAttackSpeed.ButtonUpgradeAttackSpeedPressed -= OnTryUpdgradeAttackSpeed;
            _buttonUpgradeDamage.ButtonUpgradeDamagePressed -= OnTryUpdgradeDamage;
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
                string message = LeanLocalization.GetTranslationText("Turret_level_max");

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
                string message = LeanLocalization.GetTranslationText("Not_enough_money_to_upgrade");
                _infoMessagePanel.OpenMessagePanel(message);
            }
        }

        private void OnTryUpdgradeAttackSpeed()
        {
            if (_currentTurretPresenter.GetLevelAttackSpeed() >= _maxLevelUpgrade)
            {
                string message = LeanLocalization.GetTranslationText("Turret_level_max");
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
                string message = LeanLocalization.GetTranslationText("Not_enough_money_to_upgrade");
                _infoMessagePanel.OpenMessagePanel(message);
            }
        }
    }
}