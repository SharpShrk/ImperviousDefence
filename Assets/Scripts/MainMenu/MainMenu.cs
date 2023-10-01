using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Button _optionButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private SceneSwitcher _sceneSwitcher;

    private void OnEnable()
    {
        _optionPanel.SetActive(false);
        _startButton.onClick.AddListener(OnStartClick);
        _optionButton.onClick.AddListener(OnOptionClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartClick);
        _optionButton.onClick.RemoveListener(OnOptionClick);
    }

    private void OnStartClick()
    {
        _sceneSwitcher.SwitchScene("Game");
    }

    private void OnOptionClick()
    {
        _startPanel.SetActive(false);
        _optionPanel.SetActive(true);
    }
}
