using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _score.OnScoreChanged += SetValue;
    }

    private void OnDisable()
    {
        _score.OnScoreChanged -= SetValue;
    }

    private void SetValue(int value)
    {
        _scoreText.text = value.ToString();
    }
}
