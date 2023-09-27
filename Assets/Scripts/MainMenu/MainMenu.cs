using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _optionPanel.SetActive(false);
        _exitButton.onClick.AddListener(OnExitClick);
        _optionButton.onClick.AddListener(OnOptionClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitClick);
        _optionButton.onClick.RemoveListener(OnOptionClick);
    }

    private void OnExitClick()
    {
        Application.Quit();
    }

    private void OnOptionClick()
    {
        _startPanel.SetActive(false);
        _optionPanel.SetActive(true);
    }
}
