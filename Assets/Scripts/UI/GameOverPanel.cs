using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private const string _menuSceneName = "Menu";

    [SerializeField] private GameObject _scorePanel;
    [SerializeField] private GameObject _bagUI;
    [SerializeField] private GameObject _upRightCornerUI;

    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _wavesRecord;
    [SerializeField] private TMP_Text _coinReward;
    [SerializeField] private Button _inMenuButton;
    [SerializeField] private Button _rewardButton;

    [SerializeField] private Score _score;
    [SerializeField] private Waves _waves;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameOverChecker _gameOver;

    private int _collectedMoney;
    private int _rewardMultiplier = 2;

    public event UnityAction<int> GameOvered;

    private void OnEnable()
    {
        _scorePanel.SetActive(false);

        _gameOver.GameOverEvent += InitGameOverPanel;
        _inMenuButton.onClick.AddListener(OnMenuButtonClick);
        _rewardButton.onClick.AddListener(OnRewardButtonClick);
    }

    private void OnDisable()
    {
        _gameOver.GameOverEvent -= InitGameOverPanel;
        _inMenuButton.onClick.RemoveListener(OnMenuButtonClick);
        _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
    }

    private void InitGameOverPanel()
    {
        _scorePanel.SetActive(true);
        _bagUI.SetActive(false);
        _upRightCornerUI.SetActive(false);

        _currentScore.text = _score.ScorePoints.ToString();
        _wavesRecord.text = _waves.CurrentWave.ToString();
        _coinReward.text = _wallet.Money.ToString();
        _collectedMoney = _wallet.Money;
    }

    private void OnMenuButtonClick()
    {
        GameOvered?.Invoke(_collectedMoney);
        SceneManager.LoadScene(_menuSceneName);
    }

    private void OnRewardButtonClick()
    {
        //показать рекламу, если успешно, то удвоить награду
        _collectedMoney *= _rewardMultiplier;
        _coinReward.text = _collectedMoney.ToString();
        _rewardButton.gameObject.SetActive(false);
    }
}
