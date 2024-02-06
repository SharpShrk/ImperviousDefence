using UnityEngine;
using Walls;

namespace Enemies
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private int _rewardMoney;
        [SerializeField] private int _rewardScore;

        private bool _isDied;
        private EnemyMovement _enemyMovement;
        private EnemyAttack _enemyAttack;

        public event System.Action<int, int, Enemy> OnEnemyDied;

        private void Awake()
        {
            _enemyMovement = GetComponent<EnemyMovement>();
            _enemyAttack = GetComponent<EnemyAttack>();
        }

        public void Activate()
        {
            Wall targetWall = FindClosestWall();
            _enemyAttack.SetTargetWall(targetWall);
        }

        public void AssignAttackPoint(WallAttackPoint point)
        {
            _enemyMovement.SetupAttackPoint(point);
            _enemyAttack.SetTargetWall(point.GetComponentInParent<Wall>());
        }

        private Wall FindClosestWall()
        {
            Wall[] walls = FindObjectsOfType<Wall>();
            Wall closestWall = null;
            float minDistance = float.MaxValue;

            foreach (Wall wall in walls)
            {
                float distance = Vector3.Distance(transform.position, wall.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestWall = wall;
                }
            }

            return closestWall;
        }
    }
}
