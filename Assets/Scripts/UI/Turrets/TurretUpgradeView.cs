using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretUpgradeView : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _upgradeMenu;
    [SerializeField] private GameObject _panelAttackSpeed;
    [SerializeField] private GameObject _panelDamage;
    [SerializeField] private GameObject _panelPlaceTurret;

    //сообщить презентеру, что нажата кнопка. Как сделать так, чтобы об этом узнал нужный презентер?
    //просто но тупо - создать 4 окна апдейтов. Даже не думать об этом. Очень тупо.


    private void OnEnable()
    {
        _exitButton.onClick.AddListener(DeactivateUpgradeMenu);
        _upgradeMenu.SetActive(false);
        _panelAttackSpeed.SetActive(false);
        _panelDamage.SetActive(false);
        _panelPlaceTurret.SetActive(false);
    }

    public void OpenBuyTurretMenu()
    {
        ActivateUpgradeMenu();
        _panelDamage.SetActive(false);
        _panelAttackSpeed.SetActive(false);
        _panelPlaceTurret.SetActive(true);      
    }

    public void OpenUpgradeTurretMenu(GameObject turret)
    {
        ActivateUpgradeMenu();
        _panelPlaceTurret.SetActive(false);
        _panelDamage.SetActive(true);
        _panelAttackSpeed.SetActive(true);
    }

    private void ActivateUpgradeMenu()
    {
        _upgradeMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void DeactivateUpgradeMenu()
    {
        _upgradeMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
