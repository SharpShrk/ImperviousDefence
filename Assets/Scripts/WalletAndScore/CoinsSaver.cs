using UnityEngine;

public class CoinsSaver : MonoBehaviour
{
    private const string COIN_KEY = "Coins";

    [SerializeField] private Wallet _wallet;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private GameOverHandler _gameOverPanel;

    private int _minStartMoneyValue = 100;

    private void OnEnable()
    {
        _enemySpawner.WaveCleared += SaveCoins;
        _gameOverPanel.GameOvered += SaveCoins;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveCleared += SaveCoins;
        _gameOverPanel.GameOvered -= SaveCoins;
    }

    private void Start()
    {
        LoadCoins();
    }

    private void LoadCoins()
    {
        if (PlayerPrefs.HasKey(COIN_KEY))
        {
            _wallet.SetStartValue(PlayerPrefs.GetInt(COIN_KEY));
        }
        else
        {
            _wallet.SetStartValue(_minStartMoneyValue);
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(COIN_KEY, _wallet.Money);
    }

    private void SaveCoins(int money)
    {
        PlayerPrefs.SetInt(COIN_KEY, money);
    }
}
