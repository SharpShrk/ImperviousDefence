using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] private Wall[] _walls;

    public event UnityAction GameOverEvent;

    private void OnEnable()
    {
        foreach(Wall wall in _walls)
        {
            wall.WallDestroed += GameOver;
        }
    }

    private void OnDisable()
    {
        foreach (Wall wall in _walls)
        {
            wall.WallDestroed -= GameOver;
        }
    }

    private void GameOver()
    {
        GameOverEvent?.Invoke();
        Time.timeScale = 0;
    }

}
