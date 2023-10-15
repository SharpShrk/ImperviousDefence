using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AdWarningPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private int _duration = 3;

    public event UnityAction AdCountdownFinished;

    private void Start()
    {
        _panel.SetActive(false);
    }

    public void StartAdCountdown()
    {
        Time.timeScale = 0;
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
            yield return new WaitForSecondsRealtime(1f);
            remainingTime--;
        }

        AdCountdownFinished?.Invoke();

        _panel.SetActive(false);
        Time.timeScale = 1;
    }
}