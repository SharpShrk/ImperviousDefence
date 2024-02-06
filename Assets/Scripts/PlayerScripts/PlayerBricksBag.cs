using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerBricksBag : MonoBehaviour
    {
        [SerializeField] private int _maxBrickCapacity = 5;

        private int _currentBrickCount = 0;

        public event Action<int> OnBrickCountChanging;

        public int AvailableCapacity => _maxBrickCapacity - _currentBrickCount;

        public int CurrentBrickCount => _currentBrickCount;

        public int MaxBrickCapacity => _maxBrickCapacity;

        public bool AddBricks(int count)
        {
            if (_currentBrickCount + count <= _maxBrickCapacity)
            {
                _currentBrickCount += count;
                OnBrickCountChanging?.Invoke(_currentBrickCount);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveBricks(int count)
        {
            if (_currentBrickCount - count >= 0)
            {
                _currentBrickCount -= count;
                OnBrickCountChanging?.Invoke(_currentBrickCount);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}