using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;

namespace UserInterface
{
    public class ProductionPanel : MonoBehaviour
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private InfoMessagePanel _infoPanel;
        [SerializeField] private GamePauseHandler _pauseHandler;

        public event UnityAction ButtonConfirmProductionClick;

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
            _pauseHandler.ResumeGame();
            _panel.SetActive(false);
        }

        private void OnButtonConfirmClick()
        {
            ButtonConfirmProductionClick?.Invoke();
        }
    }
}