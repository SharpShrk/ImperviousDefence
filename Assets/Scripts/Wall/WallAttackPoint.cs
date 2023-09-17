using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAttackPoint : MonoBehaviour
{
    private bool _isOccupied = false;
    private Enemy _occupyingEnemy;

    public bool IsOccupied => _isOccupied;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        if (_occupyingEnemy != null)
        {
            _occupyingEnemy.OnEnemyDiedForAttackPoint += ReleasePoint;
        }
    }

    private void UnsubscribeFromEvents()
    {
        if (_occupyingEnemy != null)
        {
            _occupyingEnemy.OnEnemyDiedForAttackPoint -= ReleasePoint;
        }
    }

    public void SetOccupied(Enemy enemy)
    {
        //UnsubscribeFromEvents();
        _isOccupied = true;
        _occupyingEnemy = enemy;
        //SubscribeToEvents();
    }

    private void ReleasePoint()
    {
        _isOccupied = false;
        //UnsubscribeFromEvents();
        _occupyingEnemy = null;
    }
}

/*public class WallAttackPoint : MonoBehaviour
{
    private bool _isOccupied = false;
    private Enemy _occupyingEnemy;

    public bool IsOccupied => _isOccupied;

    private void OnEnable()
    {
        _occupyingEnemy.OnEnemyDiedForAttackPoint += ReleasePoint;
    }

    private void OnDisable()
    {
        if (_occupyingEnemy != null)
        {
            _occupyingEnemy.OnEnemyDiedForAttackPoint -= ReleasePoint;
        }
    }

    public void SetOccupied(Enemy enemy)
    {
        _isOccupied = true;
        _occupyingEnemy = enemy;
    }

    private void ReleasePoint()
    {
        _isOccupied = false;
        _occupyingEnemy = null;
    }
}*/
