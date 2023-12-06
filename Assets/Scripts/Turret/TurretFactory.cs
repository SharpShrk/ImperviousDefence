using System.Collections.Generic;
using UnityEngine;

public class TurretFactory : MonoBehaviour
{
    [SerializeField] private BulletTurretPool turretBulletPool;
    [SerializeField] private List<GameObject> _turrets;

    private void Start()
    {
        InitTurretsBulletPool();
        DeactivateTurrets();
    }

    private void InitTurretsBulletPool()
    {
        for (int i = 0; i < _turrets.Count; i++)
        {
            _turrets[i].GetComponent<Turret>().Init(turretBulletPool);
        }
    }

    private void DeactivateTurrets()
    {
        for (int i = 0; i < _turrets.Count; i++)
        {
            _turrets[i].SetActive(false);
        }
    }
}
