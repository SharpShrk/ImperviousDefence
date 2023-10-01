using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        _placeTurretView.ButtonTurretBuyPressed += TryBuyTurret;
        _upgradeService.AttackSpeedUpgraded += SetValuesInPanelAttackSpeed;
        _upgradeService.DamageUpgraded += SetValuesInPanelDamage;

        _exitButtonPanelUpgrade.onClick.AddListener(DeactivateUpgradeMenu);
        _exitButtonPanelBuyTurret.onClick.AddListener(DeactivateUpgradeMenu);

        _upgradeMenuObject.SetActive(false);
        _panelUpgrade.SetActive(false);
        _panelPlaceTurretObject.SetActive(false);
    }

    private void OnDisable()
    {
        _placeTurretView.ButtonTurretBuyPressed -= TryBuyTurret;
        _upgradeService.AttackSpeedUpgraded -= SetValuesInPanelAttackSpeed;
        _upgradeService.DamageUpgraded -= SetValuesInPanelDamage;

        _exitButtonPanelUpgrade.onClick.RemoveListener(DeactivateUpgradeMenu);
        _exitButtonPanelBuyTurret.onClick.RemoveListener(DeactivateUpgradeMenu);
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

        SetValuesInPanelAttackSpeed();
        SetValuesInPanelDamage();
    }

    private void ActivateUpgradeMenu()
    {
        _upgradeMenuObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void DeactivateUpgradeMenu()
    {
        _currentTurretPresenter = null;

        _upgradeMenuObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetValuesInPanelAttackSpeed()
    {
        int level = _currentTurretPresenter.GetLevelAttackSpeed();
        int cost = _upgradeService.GetCostUpgrade(level);

        _panelAttackSpeed.SetCostValue(cost);
        _panelAttackSpeed.SetLevelValue(level);
    }

    private void SetValuesInPanelDamage()
    {
        int level = _currentTurretPresenter.GetLevelDamage();
        int cost = _upgradeService.GetCostUpgrade(level);

        _panelDamage.SetCostValue(cost);
        _panelDamage.SetLevelValue(level);
    }

    private void TryBuyTurret()
    {
        bool success = _wallet.SpendMoney(_placeTurretView.CostValue);

        if (success)
        {
            _currentTurretPresenter.TurretPlace();
            DeactivateUpgradeMenu();
        }
        else
        {
            string message = "Не достаточно денег для покупки турели!";
            _infoMessagePanel.OpenMessagePanel(message);
        } 
    }
}
