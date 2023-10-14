using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    [SerializeField] private int _adLevelIndex;
    [SerializeField] private Waves _waves;
    [SerializeField] private EnemySpawner _enemySpawner;
    //[SerializeField] private PlayerData _playerData;
    private AudioManager _audioManager;

    //узнать как запускать промежуточную рекламу
    //сделать метод, который будет получать каждый раз после зачистки волны номер волны
    //если номер волны каждый допустим 3 или 4, то запустить панельку, которая предупредит, что через 3, 2, 1 будет показ рекламы. 
    //дождаться завршения корутины. Запустить просмотр рекламы

    private bool _adIsPlaying;

    public bool AdIsPlaying => _adIsPlaying;

    public event UnityAction VideoAdPlayed; //ивент что реклама посмотрена

    private void OnEnable()
    {
        _enemySpawner.WaveCleared += TryShowInterAd;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveCleared -= TryShowInterAd;
    }

    private void TryShowInterAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayRegularAdIf(ShouldPlayAd());
#endif
    }

    public void ShowVideoAd()
    {       
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnPlayed, OnRewarded,OnClosed);
#endif
    }

    private void ShowInterstitialAd()
    {        
        InterstitialAd.Show(OnPlayed, OnClosedInterstitialAd);
    }

    private void OnRewarded()
    {
        VideoAdPlayed?.Invoke();
    }

    private void OnClosed()
    {
        _audioManager.RestorePreviousVolumeAndUnpause();
        _adIsPlaying = false;
    }

    private void OnPlayed()
    {
        _audioManager.MuteAllAndPause();
        _adIsPlaying = true;
    }

    private void PlayRegularAdIf(bool value)
    {
        //сделать корутину, которая предупредит о рекламе
        if (value)
        {
            ShowInterstitialAd();
        }
    }

    private bool ShouldPlayAd()
    {
        return _waves.CurrentWave % _adLevelIndex == 0;
    }

    private void OnClosedInterstitialAd(bool value)
    {
        _audioManager.RestorePreviousVolumeAndUnpause();
        _adIsPlaying = false;
    }
}