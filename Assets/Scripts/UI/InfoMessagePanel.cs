using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMessagePanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Canvas[] _guiCanvases;

    private bool _isPaused = false;

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

        if (Time.timeScale == 0)
        {
            _isPaused = true;
        }      
        else
        {
            Time.timeScale = 0;
        } 
    }

    private void OnCloseButtonClick()
    {
        _panel.SetActive(false);

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
