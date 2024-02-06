using LeaderBoard;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WalletAndScore;
using Wave;
using Yandex;

namespace UserInterface
{
    public class GameOverHandler : MonoBehaviour
    {
        private const string _menuSceneName = "Menu";

        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _bagUI;
        [SerializeField] private GameObject _upRightCornerUI;
        [SerializeField] private GameObject _pauseButtonUI;

        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private TMP_Text _wavesRecord;
        [SerializeField] private TMP_Text _coinReward;
        [SerializeField] private Button _inMenuButton;
        [SerializeField] private Button _rewardButton;

        [SerializeField] private Score _score;
        [SerializeField] private Waves _waves;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private GameOverChecker _gameOver;
        [SerializeField] private AdPlayer _adPlayer;

        private LeaderboardLoader _leaderboardLoader;

        public event Action<int> GameOvered;

        private void OnEnable()
        {
            _scorePanel.SetActive(false);

            _inMenuButton.onClick.AddListener(OnMenuButtonClick);
            _rewardButton.onClick.AddListener(OnRewardButtonClick);
            _gameOver.GameOvered += OnInitGameOverPanel;
            _adPlayer.VideoAdPlayed += OnRewardForAds;
        }

        private void OnDisable()
        {
            _gameOver.GameOvered -= OnInitGameOverPanel;
            _inMenuButton.onClick.RemoveListener(OnMenuButtonClick);
            _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
            _adPlayer.VideoAdPlayed -= OnRewardForAds;
        }

        private void OnInitGameOverPanel()
        {
            Time.timeScale = 0;
            _scorePanel.SetActive(true);
            _bagUI.SetActive(false);
            _upRightCornerUI.SetActive(false);
            _pauseButtonUI.SetActive(false);

            _currentScore.text = _score.ScorePoints.ToString();
            _wavesRecord.text = _waves.CurrentWave.ToString();
            _coinReward.text = _wallet.Money.ToString();

            _leaderboardLoader = FindObjectOfType<LeaderboardLoader>();
            _leaderboardLoader.TryRunToRegisterNewMaxScore();
        }

        private void OnMenuButtonClick()
        {
            GameOvered?.Invoke(_wallet.Money);
            SceneManager.LoadScene(_menuSceneName);
        }

        private void OnRewardButtonClick()
        {
            _adPlayer.ShowVideoAd();
            _rewardButton.gameObject.SetActive(false);
        }

        private void OnRewardForAds()
        {
            _wallet.AddMoney(_wallet.Money);
            _coinReward.text = _wallet.Money.ToString();
        }
    }
}