using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackPointQueue : MonoBehaviour
{
    private Queue<Enemy> _waitingEnemies = new Queue<Enemy>();
    private List<WallAttackPoint> _availableAttackPoints = new List<WallAttackPoint>();

    private void Start()
    {
        _availableAttackPoints = GetAllAvailableAttackPoints();
    }

    public void AddEnemyToQueue(Enemy enemy)
    {
        _waitingEnemies.Enqueue(enemy);
        TryAssignAttackPoint();
    }

    public void ReleaseAttackPoint(WallAttackPoint point)
    {
        _availableAttackPoints.Add(point);

        Enemy closestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (var enemy in _waitingEnemies)
        {
            float distance = CalculateDistanceToAttackPoint(enemy, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            _waitingEnemies = new Queue<Enemy>(_waitingEnemies.Where(e => e != closestEnemy));
            closestEnemy.AssignAttackPoint(point);
            point.SetOccupied(closestEnemy);
            _availableAttackPoints.Remove(point);
        }
    }

    private float CalculateDistanceToAttackPoint(Enemy enemy, WallAttackPoint point)
    {
        return Vector3.Distance(enemy.transform.position, point.transform.position);
    }

    private void TryAssignAttackPoint()
    {
        while (_waitingEnemies.Count > 0 && _availableAttackPoints.Count > 0)
        {
            WallAttackPoint closestPoint = null;
            Enemy closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (var point in _availableAttackPoints)
            {
                foreach (var enemy in _waitingEnemies)
                {
                    float distance = CalculateDistanceToAttackPoint(enemy, point);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = enemy;
                        closestPoint = point;
                    }
                }
            }

            if (closestEnemy != null && closestPoint != null)
            {
                _waitingEnemies = new Queue<Enemy>(_waitingEnemies.Where(e => e != closestEnemy));
                closestEnemy.AssignAttackPoint(closestPoint);
                closestPoint.SetOccupied(closestEnemy);
                _availableAttackPoints.Remove(closestPoint);
            }
        }
    }

    private List<WallAttackPoint> GetAllAvailableAttackPoints()
    {
        Wall[] walls = FindObjectsOfType<Wall>();
        List<WallAttackPoint> availableAttackPoints = new ();

        foreach (var wall in walls)
        {
            for (int i = 0; i < wall.AttackPoints.Count; i++)
            {
                availableAttackPoints.Add(wall.AttackPoints[i].GetComponent<WallAttackPoint>());
            }
        }

        return availableAttackPoints;
    }
}
