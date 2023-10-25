using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _menuPausePanel;

    private void OnEnable()
    {
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
        Time.timeScale = 0;
    }

    private void ClosePausePanel()
    {
        _pauseButton.gameObject.SetActive(true);
        _menuPausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
