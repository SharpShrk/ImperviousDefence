using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerBricksBag : MonoBehaviour
    {
        [SerializeField] private int _maxBrickCapacity = 5;

        private int _currentBrickCount = 0;

        public event Action<int> BrickCountChanging;

        public int AvailableCapacity => _maxBrickCapacity - _currentBrickCount;

        public int CurrentBrickCount => _currentBrickCount;

        public int MaxBrickCapacity => _maxBrickCapacity;

        public bool AddBricks(int count)
        {
            bool canAddBricks = _currentBrickCount + count <= _maxBrickCapacity;

            if (canAddBricks)
            {
                _currentBrickCount += count;
                BrickCountChanging?.Invoke(_currentBrickCount);
            }

            return canAddBricks;
        }

        public bool RemoveBricks(int count)
        {
            bool canRemoveBricks = _currentBrickCount - count >= 0;

            if (canRemoveBricks)
            {
                _currentBrickCount -= count;
                BrickCountChanging?.Invoke(_currentBrickCount);
            }

            return canRemoveBricks;
        }
    }
}