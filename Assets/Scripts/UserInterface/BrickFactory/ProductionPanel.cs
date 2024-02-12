using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UserInterface
{
    public class ProductionPanel : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private InfoMessagePanel _infoPanel;

        public event UnityAction OnButtonConfirmProductionClick;

        private void OnEnable()
        {
            _confirmButton.onClick.AddListener(OnButtonConfirmClick);
            _cancelButton.onClick.AddListener(OnClosePanel);
        }

        private void OnDisable()
        {
            _confirmButton.onClick.RemoveListener(OnButtonConfirmClick);
            _cancelButton.onClick.RemoveListener(OnClosePanel);
        }

        private void OnClosePanel()
        {
            Time.timeScale = 1;
            _panel.SetActive(false);
        }

        private void OnButtonConfirmClick()
        {
            OnButtonConfirmProductionClick?.Invoke();
        }
    }
}