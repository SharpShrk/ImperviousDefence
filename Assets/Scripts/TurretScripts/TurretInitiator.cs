using Bullet;
using System.Collections.Generic;
using UnityEngine;

namespace TurretScripts
{
    public class TurretInitiator : MonoBehaviour
    {
        [SerializeField] private BulletTurretPool _turretBulletPool;
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
                _turrets[i].GetComponent<Turret>().Init(_turretBulletPool);
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
}