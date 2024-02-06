using Lean.Localization;
using TurretScripts;
using UnityEngine;
using UnityEngine.UI;
using WalletAndScore;

namespace UserInterface
{
    public class TurretUpgradeView : MonoBehaviour
    {
        [SerializeField] private Button _exitButtonPanelUpgrade;
        [SerializeField] private Button _exitButtonPanelBuyTurret;
        [SerializeField] private GameObject _upgradeMenuObject;
        [SerializeField] private GameObject _panelAttackSpeedObject;
        [SerializeField] private GameObject _panelDamageObject;
        [SerializeField] private GameObject _panelPlaceTurretObject;
        [SerializeField] private GameObject _panelUpgrade;
        [SerializeField] private UpgradeService _upgradeService;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private InfoMessagePanel _infoMessagePanel;

        private TurretPresenter _currentTurretPresenter;
        private PlaceTurretView _placeTurretView;
        private TurretButtonUpgradeAttackSpeed _panelAttackSpeed;
        private TurretButtonUpgradeDamage _panelDamage;

        private void OnEnable()
        {
            _placeTurretView = _panelPlaceTurretObject.GetComponent<PlaceTurretView>();
            _panelAttackSpeed = _panelAttackSpeedObject.GetComponent<TurretButtonUpgradeAttackSpeed>();
            _panelDamage = _panelDamageObject.GetComponent<TurretButtonUpgradeDamage>();

            _placeTurretView.ButtonTurretBuyPressed += OnTryBuyTurret;
            _upgradeService.AttackSpeedUpgraded += OnSetValuesInPanelAttackSpeed;
            _upgradeService.DamageUpgraded += OnSetValuesInPanelDamage;

            _exitButtonPanelUpgrade.onClick.AddListener(OnDeactivateUpgradeMenu);
            _exitButtonPanelBuyTurret.onClick.AddListener(OnDeactivateUpgradeMenu);

            _upgradeMenuObject.SetActive(false);
            _panelUpgrade.SetActive(false);
            _panelPlaceTurretObject.SetActive(false);
        }

        private void OnDisable()
        {
            _placeTurretView.ButtonTurretBuyPressed -= OnTryBuyTurret;
            _upgradeService.AttackSpeedUpgraded -= OnSetValuesInPanelAttackSpeed;
            _upgradeService.DamageUpgraded -= OnSetValuesInPanelDamage;

            _exitButtonPanelUpgrade.onClick.RemoveListener(OnDeactivateUpgradeMenu);
            _exitButtonPanelBuyTurret.onClick.RemoveListener(OnDeactivateUpgradeMenu);
        }

        public void OpenBuyTurretMenu(TurretPresenter turretPresenter)
        {
            if (turretPresenter == null)
                return;

            _currentTurretPresenter = turretPresenter;

            ActivateUpgradeMenu();
            _panelUpgrade.SetActive(false);
            _panelPlaceTurretObject.SetActive(true);
        }

        public void OpenUpgradeTurretMenu(TurretPresenter turretPresenter)
        {
            if (turretPresenter == null)
                return;

            _currentTurretPresenter = turretPresenter;

            ActivateUpgradeMenu();
            _panelPlaceTurretObject.SetActive(false);
            _panelUpgrade.SetActive(true);

            OnSetValuesInPanelAttackSpeed();
            OnSetValuesInPanelDamage();
        }

        private void ActivateUpgradeMenu()
        {
            _upgradeMenuObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnDeactivateUpgradeMenu()
        {
            _currentTurretPresenter = null;

            _upgradeMenuObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void OnSetValuesInPanelAttackSpeed()
        {
            int level = _currentTurretPresenter.GetLevelAttackSpeed();
            int cost = _upgradeService.GetCostUpgrade(level);

            _panelAttackSpeed.SetCostValue(cost);
            _panelAttackSpeed.SetLevelValue(level);
        }

        private void OnSetValuesInPanelDamage()
        {
            int level = _currentTurretPresenter.GetLevelDamage();
            int cost = _upgradeService.GetCostUpgrade(level);

            _panelDamage.SetCostValue(cost);
            _panelDamage.SetLevelValue(level);
        }

        private void OnTryBuyTurret()
        {
            bool success = _wallet.SpendMoney(_placeTurretView.CostValue);

            if (success)
            {
                _currentTurretPresenter.TurretPlace();
                OnDeactivateUpgradeMenu();
            }
            else
            {
                string message = LeanLocalization.GetTranslationText("Not_enough_money_to_buy_turret");
                _infoMessagePanel.OpenMessagePanel(message);
            }
        }
    }
}