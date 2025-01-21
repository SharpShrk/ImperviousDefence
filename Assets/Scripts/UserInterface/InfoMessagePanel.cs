using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UserInterface
{
    public class InfoMessagePanel : BasePauseCanvas
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private GameObject _panel;

        private void OnEnable()
        {
            _panel.SetActive(false);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        }

        public void OpenMessagePanel(string message)
        {
            _message.text = message;
            _panel.SetActive(true);
            HideGUICanvas();
            PauseGameIfNotPaused();
        }

        private void OnCloseButtonClick()
        {
            _panel.SetActive(false);
            OpenGUICanvas();
            ResumeGameIfPaused();
        }
    }
}