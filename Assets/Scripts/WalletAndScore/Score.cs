using UnityEngine;
using UnityEngine.Events;

namespace WalletAndScore
{
    public class Score : MonoBehaviour
    {
        private int _score;

        public event UnityAction<int> OnScoreChanged;

        public int ScorePoints => _score;

        private void Start()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void AddScore(int amount)
        {
            _score += amount;
            OnScoreChanged?.Invoke(_score);
        }
    }
}