using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Yandex
{
    public class AdWarningPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private int _duration = 3;
        [SerializeField] private GamePauseHandler _pauseHandler;

        private float _countdownInterval = 1f;
        private WaitForSecondsRealtime _waitCountdownInterval;

        public event Action AdCountdownFinished;

        private void Start()
        {
            _panel.SetActive(false);
            _waitCountdownInterval = new WaitForSecondsRealtime(_countdownInterval);
        }

        public void StartAdCountdown()
        {
            _pauseHandler.PauseGame();
            _panel.SetActive(true);
            _timerText.text = _duration.ToString();

            StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            int remainingTime = _duration;

            while (remainingTime > 0)
            {
                _timerText.text = remainingTime.ToString();
                yield return _waitCountdownInterval;
                remainingTime--;
            }

            AdCountdownFinished?.Invoke();

            _panel.SetActive(false);
            _pauseHandler.ResumeGame();
        }
    }
}