using UnityEngine;
using Walls;

namespace Enemies
{
    [RequireComponent(typeof(EnemyMovement))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyBehavior : MonoBehaviour
    {
        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        public void AssignAttackPoint(WallAttackPoint point)
        {
            _enemyMovement.SetupAttackPoint(point);
            _enemyAttack.SetTargetWall(point.GetComponentInParent<Wall>());
        }
    }
}