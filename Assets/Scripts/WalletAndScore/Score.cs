using UnityEngine;
using UnityEngine.Events;
using UIReward;
using System;

namespace WalletAndScore
{
    public class Score : MonoBehaviour, IReward
    {
        private int _score;

        public static event UnityAction<Score> ObjectLoaded;
        public event Action<int> ValueChanged;

        public int ScorePoints => _score;

        private void Start()
        {
            _score = 0;
            ValueChanged?.Invoke(_score);
            ObjectLoaded?.Invoke(this);
        }

        public void AddScore(int amount)
        {
            _score += amount;
            ValueChanged?.Invoke(_score);
        }
    }
}