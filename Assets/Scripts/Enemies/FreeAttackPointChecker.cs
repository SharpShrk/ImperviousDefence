using System.Collections.Generic;
using UnityEngine;
using Walls;

namespace Enemies
{
    public class FreeAttackPointChecker : MonoBehaviour
    {
        [SerializeField] private WallContainer _wallContainer;

        private List<WallAttackPoint> _attackPoints = new();

        private void Start()
        {
            FindWallsAndAttackPoints();
        }

        private void FindWallsAndAttackPoints()
        {
            foreach (var wall in _wallContainer.Walls)
            {
                for (int i = 0; i < wall.AttackPoints.Count; i++)
                {
                    _attackPoints.Add(wall.AttackPoints[i].GetComponent<WallAttackPoint>());
                }
            }
        }
    }
}