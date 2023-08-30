using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class TurretUpgrade : MonoBehaviour
{    
    [SerializeField] protected int StartCostValue;
    [SerializeField] protected int StartLevelValue;

    protected int LevelValue;
    protected int CostValue;

    //переделать. Задача такая. У каждой турели хранятся данные об ее апдейтах.
    //Скорость атаки текущая. Уровень апдейта. Стоимость апдейта. При открытии окна апдейта происходит запрос у турели этих данных и отправляется в UI.
    //аналогично с уроном.

    protected void OnEnable()
    {
        LevelValue = StartLevelValue;
        CostValue = StartCostValue;       
    }

    protected abstract void OnButtonClick();

    public void SetCostValue(int value) //сделать приватным, получить данные об апгрейде по событию
    {
        CostValue = value; //передать значение в UI
    }

    public void SetLevelValue() //сделать приватным, получить данные об апгрейде по событию
    {
        LevelValue++; //передать значение в UI
    }
}
