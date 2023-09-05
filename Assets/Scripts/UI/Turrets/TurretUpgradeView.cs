using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurretUpgradeView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _upgradeMenuObject;
    [SerializeField] private GameObject _panelAttackSpeedObject;
    [SerializeField] private GameObject _panelDamageObject;
    [SerializeField] private GameObject _panelPlaceTurretObject;
    [SerializeField] private UpgradeService _upgradeService;

    private TurretPresenter _currentTurretPresenter;
    private PlaceTurretView _placeTurretView;
    TurretButtonUpgradeAttackSpeed _panelAttackSpeed;
    TurretButtonUpgradeDamage _panelDamage;

    private void OnEnable()
    {
        _placeTurretView = _panelPlaceTurretObject.GetComponent<PlaceTurretView>();
        _panelAttackSpeed = _panelAttackSpeedObject.GetComponent<TurretButtonUpgradeAttackSpeed>();
        _panelDamage = _panelDamageObject.GetComponent<TurretButtonUpgradeDamage>();

        _placeTurretView.ButtonTurretBuyPressed += TryBuyTurret;
        _upgradeService.AttackSpeedUpgraded += SetValuesInPanelAttackSpeed;
        _upgradeService.DamageUpgraded += SetValuesInPanelDamage;

        _exitButton.onClick.AddListener(DeactivateUpgradeMenu);

        _upgradeMenuObject.SetActive(false);
        _panelAttackSpeedObject.SetActive(false);
        _panelDamageObject.SetActive(false);
        _panelPlaceTurretObject.SetActive(false);
    }

    private void OnDisable()
    {
        _placeTurretView.ButtonTurretBuyPressed -= TryBuyTurret;
        _upgradeService.AttackSpeedUpgraded -= SetValuesInPanelAttackSpeed;
        _upgradeService.DamageUpgraded -= SetValuesInPanelDamage;

        _exitButton?.onClick.RemoveListener(DeactivateUpgradeMenu);
    }

    public void OpenBuyTurretMenu(TurretPresenter turretPresenter)
    {
        if (turretPresenter == null)
            return;

        _currentTurretPresenter = turretPresenter;

        ActivateUpgradeMenu();
        _panelDamageObject.SetActive(false);
        _panelAttackSpeedObject.SetActive(false);
        _panelPlaceTurretObject.SetActive(true);      
    }

    public void OpenUpgradeTurretMenu(TurretPresenter turretPresenter)
    {
        if (turretPresenter == null)
            return;

        _currentTurretPresenter = turretPresenter;

        ActivateUpgradeMenu();
        _panelPlaceTurretObject.SetActive(false);
        _panelDamageObject.SetActive(true);
        _panelAttackSpeedObject.SetActive(true);

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
        //оформить покупку. Если все оки, ставим турель
        //перенести в апгрейд сервис или создать еще один метод? уфффф

        _currentTurretPresenter.TurretPlace();
        DeactivateUpgradeMenu();
    }
}
