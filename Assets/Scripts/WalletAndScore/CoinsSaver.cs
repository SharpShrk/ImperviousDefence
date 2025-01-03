using Enemies;
using UnityEngine;
using UserInterface;

namespace WalletAndScore
{
    public class CoinsSaver : MonoBehaviour
    {
        private const string CoinKey = "Coins";

        [SerializeField] private Wallet _wallet;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private GameOverHandler _gameOverPanel;

        private int _minStartMoneyValue = 100;

        private void OnEnable()
        {
            _enemySpawner.WaveCleared += OnSaveCoins;
            _gameOverPanel.GameOvered += OnSaveCoins;
        }

        private void OnDisable()
        {
            _enemySpawner.WaveCleared += OnSaveCoins;
            _gameOverPanel.GameOvered -= OnSaveCoins;
        }

        private void Start()
        {
            LoadCoins();
        }

        public void OnSaveCoins()
        {
            PlayerPrefs.SetInt(CoinKey, _wallet.Money);
        }

        private void OnSaveCoins(int money)
        {
            PlayerPrefs.SetInt(CoinKey, money);
        }

        private void LoadCoins()
        {
            if (PlayerPrefs.HasKey(CoinKey))
            {
                _wallet.SetStartValue(PlayerPrefs.GetInt(CoinKey));
            }
            else
            {
                _wallet.SetStartValue(_minStartMoneyValue);
            }
        }
    }
}