using Agava.YandexGames;
using UnityEngine;
using UnityEngine.Events;

public class AdPlayer : MonoBehaviour
{
    [SerializeField] private int _adLevelIndex;
    [SerializeField] private Waves _waves;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AdWarningPanel _adWarningPanel;

    private bool _adIsPlaying;

    public bool AdIsPlaying => _adIsPlaying;

    public event UnityAction VideoAdPlayed;

    private void OnEnable()
    {
        _enemySpawner.WaveCleared += TryShowInterAd;
        _adWarningPanel.AdCountdownFinished += ShowInterstitialAd;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveCleared -= TryShowInterAd;
        _adWarningPanel.AdCountdownFinished -= ShowInterstitialAd;
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
        AudioManager.Instance.RestorePreviousVolumeAndUnpause();
    }

    private void OnClosed()
    {
        AudioManager.Instance.RestorePreviousVolumeAndUnpause();
        _adIsPlaying = false;
    }

    private void OnPlayed()
    {
        AudioManager.Instance.MuteAllAndPause();
        _adIsPlaying = true;
    }

    private void PlayRegularAdIf(bool value)
    {
        if (value)
        {
            _adWarningPanel.StartAdCountdown();            
        }
    }

    private bool ShouldPlayAd()
    {
        return _waves.CurrentWave % _adLevelIndex == 0;
    }

    private void OnClosedInterstitialAd(bool value)
    {
        AudioManager.Instance.RestorePreviousVolumeAndUnpause();
        _adIsPlaying = false;
    }
}