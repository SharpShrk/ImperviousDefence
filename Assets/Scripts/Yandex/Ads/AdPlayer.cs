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

    //������ ��� ��������� ������������� �������
    //������� �����, ������� ����� �������� ������ ��� ����� �������� ����� ����� �����
    //���� ����� ����� ������ �������� 3 ��� 4, �� ��������� ��������, ������� �����������, ��� ����� 3, 2, 1 ����� ����� �������. 
    //��������� ��������� ��������. ��������� �������� �������

    private bool _adIsPlaying;

    public bool AdIsPlaying => _adIsPlaying;

    public event UnityAction VideoAdPlayed; //����� ��� ������� ����������

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
        //������� ��������, ������� ����������� � �������
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