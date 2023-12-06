using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _menuPausePanel;
    [SerializeField] private Canvas[] _guiCanvases;

    private bool _isPaused = false;

    private void OnEnable()
    {
        _pauseButton.gameObject.SetActive(true);
        _pauseButton.onClick.AddListener(OpenPausePanel);
        _closeButton.onClick.AddListener(ClosePausePanel);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OpenPausePanel);
        _closeButton.onClick.RemoveListener(ClosePausePanel);
    }

    private void OpenPausePanel()
    {
        _pauseButton.gameObject.SetActive(false);
        _menuPausePanel.gameObject.SetActive(true);

        HideGUICanvas();

        if (Time.timeScale == 0)
        {
            _isPaused = true;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    private void ClosePausePanel()
    {
        _pauseButton.gameObject.SetActive(true);
        _menuPausePanel.gameObject.SetActive(false);

        OpenGUICanvas();

        if (_isPaused == false)
        {
            Time.timeScale = 1;
        }

        _isPaused = false;
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
