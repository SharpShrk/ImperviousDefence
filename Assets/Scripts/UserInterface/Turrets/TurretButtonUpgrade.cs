using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TurretsUI
{
    public abstract class TurretButtonUpgrade : MonoBehaviour
    {
        [SerializeField] protected Button ButtonUpgrade;
        [SerializeField] protected TMP_Text CostText;
        [SerializeField] protected TMP_Text LevelText;
        [SerializeField] protected GameObject UpgradePanel;

        public event UnityAction ButtonUpgradePressed;

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

        protected virtual void OnButtonClick()
        {
            ButtonUpgradePressed?.Invoke();
        }

        protected void ActivateUpgradeMenu()
        {
            UpgradePanel.SetActive(true);
        }
    }
}