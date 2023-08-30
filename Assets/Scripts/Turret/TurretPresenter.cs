using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPresenter : MonoBehaviour
{
    [SerializeField] private Turret _turret;
    [SerializeField] private TurretAttackSpeed _turretAttackSpeed;
    [SerializeField] private TurretDamage _turretDamage;
    [SerializeField] private ColorChangeButtonUpgrade _colorChanger;
    [SerializeField] private TurretUpgradeView _upgradeMenu;
    [SerializeField] private TurretFactory _turretFactory;

    private void OnEnable()
    {
        _colorChanger.ButtonFullPressed += OpenShop;
    }

    private void OnDisable()
    {
        _colorChanger.ButtonFullPressed -= OpenShop;
    }

    //запросить у турели текущие уровни апгрейдов и установлена ли она
    //передать в ЮИ о том, какая турель открыта, какие у нее текущие уровни апгрейдов
    //получить от ЮИ информацию о том, был ли произведен апгрейд
    //собщить турели о том, что она улучшена

    private void OpenShop() //переименовать
    {
        if(_turret.IsPlaced)
        {           
            _upgradeMenu.OpenUpgradeTurretMenu(this);
        }
        else
        {
            _upgradeMenu.OpenBuyTurretMenu(this);
        }
    }

    public void TurretPlace()
    {
        if (_turret.IsPlaced == true)
            return;

        Debug.Log("Турель куплена, момент, ща установим.");
        _turret.PlaceTurret();        
    }
}
