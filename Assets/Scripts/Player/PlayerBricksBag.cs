using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBricksBag : MonoBehaviour
{
    [SerializeField] private int _maxBrickCapacity = 5;
    private int _currentBrickCount = 0;

    public int AvailableCapacity => _maxBrickCapacity - _currentBrickCount;
    public int CurrentBrickCount => _currentBrickCount;
    public int MaxBrickCapacity => _maxBrickCapacity;

    public event Action<int> OnBrickCountChanged;


    public bool AddBricks(int count)
    {
        if (_currentBrickCount + count <= _maxBrickCapacity)
        {
            _currentBrickCount += count;
            OnBrickCountChanged?.Invoke(_currentBrickCount);
            Debug.Log("Кирпчей в сумке: " + _currentBrickCount + ". Взято кирпичей: " + count);
            return true;
        }
        else
        {
            Debug.Log("Сумка полна!");
            return false;
        }
    }

    public bool RemoveBricks(int count)
    {
        if (_currentBrickCount - count >= 0)
        {
            _currentBrickCount -= count;
            OnBrickCountChanged?.Invoke(_currentBrickCount);
            return true;
        }
        else
        {
            Debug.Log("Недостаточно кирпичей в сумке!");
            return false;
        }
    }
}
