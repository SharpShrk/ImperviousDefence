using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] private Wall[] _walls;
    [SerializeField] private GameObject _textGameOver;

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
        GameOverUI(); //������� �����
    }

    //��������� �����
    private void GameOverUI()
    {
        _textGameOver.SetActive(true);
        Time.timeScale = 0;
    }
}