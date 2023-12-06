using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TurretButtonUpgrade : MonoBehaviour
{
    [SerializeField] protected Button ButtonUpgrade;
    [SerializeField] protected TMP_Text CostText;
    [SerializeField] protected TMP_Text LevelText;
    [SerializeField] protected GameObject UpgradePanel;

    protected void OnEnable()
    {
        ButtonUpgrade.onClick.AddListener(OnButtonClick);
    }

    protected void OnDisable()
    {
        ButtonUpgrade?.onClick.RemoveListener(OnButtonClick);
    }

    public void SetCostValue(int value)
    {
        CostText.text = value.ToString();
    }

    public void SetLevelValue(int value)
    {
        LevelText.text = value.ToString();
    }

    private void DeactivateUpgradeMenu()
    {
        UpgradePanel.SetActive(false);
    }

    protected abstract void OnButtonClick();

    protected void ActivateUpgradeMenu()
    {
        UpgradePanel.SetActive(true);
    }
}