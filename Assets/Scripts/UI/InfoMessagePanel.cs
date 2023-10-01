using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoMessagePanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private TMP_Text _message;
    [SerializeField] private GameObject _panel;

    private void OnEnable()
    {
        _panel.SetActive(false);
        _closeButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(OnButtonClick);
    }

    public void OpenMessagePanel(string message)
    {
        //Time.timeScale = 0;
        _message.text = message;
        _panel.SetActive(true);
    }

    private void OnButtonClick()
    {
        _panel.SetActive(false);
        //Time.timeScale = 1;
    }
}
