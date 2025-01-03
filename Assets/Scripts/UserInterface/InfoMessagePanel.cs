using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InfoMessagePanel : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Canvas[] _guiCanvases;
        [SerializeField] private GamePauseHandler _gamePauseHandler;

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

            if (_gamePauseHandler.IsPaused == false)
            {
                _gamePauseHandler.PauseGame();
            }
        }

        private void OnCloseButtonClick()
        {
            _panel.SetActive(false);

            OpenGUICanvas();

            if (_gamePauseHandler.IsPaused == false)
            {
                _gamePauseHandler.ResumeGame();
            }
        }

        private void HideGUICanvas()
        {
            foreach (var canvas in _guiCanvases)
            {
                canvas.enabled = false;
            }
        }

        private void OpenGUICanvas()
        {
            foreach (var canvas in _guiCanvases)
            {
                canvas.enabled = true;
            }
        }
    }
}