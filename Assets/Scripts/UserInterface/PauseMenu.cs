using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace UserInterface
{
    public class PauseMenu : BasePauseCanvas
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _menuPausePanel;

        private void OnEnable()
        {
            _pauseButton.gameObject.SetActive(true);
            _pauseButton.onClick.AddListener(OnOpenPausePanel);
            _closeButton.onClick.AddListener(OnClosePausePanel);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OnOpenPausePanel);
            _closeButton.onClick.RemoveListener(OnClosePausePanel);
        }

        private void OnOpenPausePanel()
        {
            _pauseButton.gameObject.SetActive(false);
            _menuPausePanel.SetActive(true);
            HideGUICanvas();
            PauseGameIfNotPaused();
        }

        private void OnClosePausePanel()
        {
            _pauseButton.gameObject.SetActive(true);
            _menuPausePanel.SetActive(false);
            OpenGUICanvas();
            ResumeGameIfPaused();
        }
    }
}
