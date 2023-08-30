using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonUpgrade : MonoBehaviour
{
    [SerializeField] protected Button ButtonUpgrade;
    [SerializeField] protected TMP_Text CostText;
    [SerializeField] protected TMP_Text LevelText;
    [SerializeField] protected GameObject UpgradePanel;

    protected void OnEnable()
    {
        ButtonUpgrade.onClick.AddListener(OnButtonClick);        
    }

    public void SetCostValue(int value) //подписаться на событие
    {        
        CostText.text = value.ToString();
    }

    public void SetLevelValue(int value)
    {
        LevelText.text = value.ToString();
    }

    protected void OnButtonClick()
    {
        //сообщить презентеру что кнопка нажата
    }

    protected void ActivateUpgradeMenu()
    {
        UpgradePanel.SetActive(true);
    }

    private void DeactivateUpgradeMenu()
    {
        UpgradePanel.SetActive(false);
    }
}
