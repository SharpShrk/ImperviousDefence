using System;
using UnityEngine;
using Utilities;
using Walls;

namespace UserInterface
{
    public class GameOverChecker : MonoBehaviour
    {
        [SerializeField] private Wall[] _walls;
        [SerializeField] private GamePauseHandler _pauseHandler;

        private bool _isGameOver = false;

        public bool IsGameOver => _isGameOver;

        public event Action GameOvered;

        private void OnEnable()
        {
            foreach (Wall wall in _walls)
            {
                wall.WallDestroed += OnGameOver;
            }
        }

        private void OnDisable()
        {
            foreach (Wall wall in _walls)
            {
                wall.WallDestroed -= OnGameOver;
            }
        }

        private void OnGameOver()
        {
            GameOvered?.Invoke();
            _isGameOver = true;
            _pauseHandler.PauseGame();
        }
    }
}