using UnityEngine;

public class CoinsSaver : MonoBehaviour
{
    private const string CoinKey = "Coins";

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

    public void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinKey, _wallet.Money);
    }

    private void SaveCoins(int money)
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
