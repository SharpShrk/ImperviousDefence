using UnityEngine;

namespace Utilities
{
    public class GamePauseHandler : MonoBehaviour
    {
        private bool _isPaused;

        public bool IsPaused => _isPaused;

        public void PauseGame()
        {
            if (_isPaused) return;

            Time.timeScale = 0f;
            _isPaused = true;
        }

        public void ResumeGame()
        {
            if (!_isPaused) return;

            Time.timeScale = 1f;
            _isPaused = false;
        }
    }
}