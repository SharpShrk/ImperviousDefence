using System.Collections.Generic;
using UnityEngine;

public class FreeAttackPointChecker : MonoBehaviour
{
    private Wall[] _walls;
    private List<WallAttackPoint> _attackPoints = new ();
    private bool _isAllAttackPointsOccupied;

    private void Start()
    {
        _isAllAttackPointsOccupied = false;
        FindWallsAndAttackPoints();
    }

    public bool CheckAttackPoints(int countAliveEnemies)
    {
        if (_attackPoints.Count == countAliveEnemies)
        {
            _isAllAttackPointsOccupied = true;
        }
        else
        {
            _isAllAttackPointsOccupied = false;
        }

        return _isAllAttackPointsOccupied;
    }

    private void FindWallsAndAttackPoints()
    {
        _walls = FindObjectsOfType<Wall>();

        foreach (var wall in _walls)
        {
            for (int i = 0; i < wall.AttackPoints.Count; i++)
            {
                _attackPoints.Add(wall.AttackPoints[i].GetComponent<WallAttackPoint>());
            }
        }
    }
}
