using Enemies;
using UnityEngine;

namespace Walls
{
    public class WallAttackPoint : MonoBehaviour
    {
        [SerializeField] private AttackPointQueue _attackPointQueue;

        private Enemy _occupyingEnemy;

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
            _occupyingEnemy = enemy;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (_occupyingEnemy != null)
            {
                _occupyingEnemy.EnemyDeath += OnReleasePoint;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (_occupyingEnemy != null)
            {
                _occupyingEnemy.EnemyDeath -= OnReleasePoint;
            }
        }

        private void OnReleasePoint()
        {
            UnsubscribeFromEvents();
            _attackPointQueue.ReleaseAttackPoint(this);
            _occupyingEnemy = null;
        }
    }
}