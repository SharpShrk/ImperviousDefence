using Enemies;
using UnityEngine;

namespace Walls
{
    public class WallAttackPoint : MonoBehaviour
    {
        private AttackPointQueue _attackPointQueue;
        private bool _isOccupied = false;
        private Enemy _occupyingEnemy;

        public bool IsOccupied => _isOccupied;

        private void Start()
        {
            _attackPointQueue = FindObjectOfType<AttackPointQueue>();
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void SetOccupied(Enemy enemy)
        {
            UnsubscribeFromEvents();
            _isOccupied = true;
            _occupyingEnemy = enemy;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (_occupyingEnemy != null)
            {
                _occupyingEnemy.OnEnemyDeath += ReleasePoint;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (_occupyingEnemy != null)
            {
                _occupyingEnemy.OnEnemyDeath -= ReleasePoint;
            }
        }

        private void ReleasePoint()
        {
            _isOccupied = false;
            UnsubscribeFromEvents();
            _attackPointQueue.ReleaseAttackPoint(this);
            _occupyingEnemy = null;
        }
    }
}