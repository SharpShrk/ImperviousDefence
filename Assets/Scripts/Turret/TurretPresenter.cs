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
    [SerializeField] private PlaceTurretView _placeTurretView;
    [SerializeField] private TurretFactory _turretFactory;

    private void OnEnable()
    {
        _colorChanger.ButtonFullPressed += OpenShop;
        //_placeTurretView.TurretBuyed += InitTurretPlace; //определить какую турель апгрейдили
    }

    //запросить у турели текущие уровни апгрейдов и установлена ли она
    //передать в ЮИ о том, какая турель открыта, какие у нее текущие уровни апгрейдов
    //получить от ЮИ информацию о том, был ли произведен апгрейд
    //собщить турели о том, что она улучшена

    private void OpenShop() //переименовать
    {
        if(_turret.IsPlaced)
        {
            //сообщить окну текущие уровни, нельзя передавать саму турель
        }
        else
        {
            //открыть окно с покупкой турели? как узнать что ее купили?
            _upgradeMenu.OpenBuyTurretMenu();
        }
    }

    private void InitTurretPlace()
    {

    }
}
