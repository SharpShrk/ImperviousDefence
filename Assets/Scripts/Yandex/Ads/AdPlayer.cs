using Agava.YandexGames;
using Audio;
using Enemies;
using System;
using UnityEngine;
using Wave;

namespace Yandex
{
    public class AdPlayer : MonoBehaviour
    {
        [SerializeField] private int _adLevelIndex;
        [SerializeField] private Waves _waves;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private AdWarningPanel _adWarningPanel;

        private bool _adIsPlaying;
        private VolumeHandler _audioResources;

        public event Action VideoAdPlayed;

        public bool AdIsPlaying => _adIsPlaying;

        private void OnEnable()
        {
            _audioResources = gameObject.GetComponent<VolumeHandler>();
            _enemySpawner.WaveCleared += OnTryShowInterAd;
            _adWarningPanel.AdCountdownFinished += OnShowInterstitialAd;
        }

        private void OnDisable()
        {
            _enemySpawner.WaveCleared -= OnTryShowInterAd;
            _adWarningPanel.AdCountdownFinished -= OnShowInterstitialAd;
        }

        public void ShowVideoAd()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        VideoAd.Show(OnPlayed, OnRewarded,OnClosed);
#endif
        }

        private void OnTryShowInterAd()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayRegularAdIf(ShouldPlayAd());
#endif
        }

        private void OnShowInterstitialAd()
        {
            InterstitialAd.Show(OnPlayed, OnClosedInterstitialAd);
        }

        private void OnRewarded()
        {
            VideoAdPlayed?.Invoke();
        }

        private void OnClosed()
        {
            _audioResources.UnmuteAndResume();
            _adIsPlaying = false;
        }

        private void OnPlayed()
        {
            _audioResources.MuteAndPause();
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
            _audioResources.UnmuteAndResume();
            _adIsPlaying = false;
        }
    }
}