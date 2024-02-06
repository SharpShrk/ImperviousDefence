using System;
using UnityEngine;
using Walls;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] private Wall[] _walls;

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
        Time.timeScale = 0;
    }
}