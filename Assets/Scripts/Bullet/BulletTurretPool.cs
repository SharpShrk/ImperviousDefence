using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class BulletTurretPool : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private int _initialPoolSize = 10;
        [SerializeField] private GameObject _bulletContainer;

        private Queue<GameObject> _bulletPool;

        private void Awake()
        {
            InitializePool();
        }

        public GameObject GetBullet()
        {
            if (_bulletPool.Count > 0)
            {
                GameObject bullet = _bulletPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.SetParent(_bulletContainer.transform);

                return bullet;
            }

            GameObject newBullet = Instantiate(_bulletPrefab);
            return newBullet;
        }

        public void ReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }

        private void InitializePool()
        {
            _bulletPool = new Queue<GameObject>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.GetComponent<BulletTurret>().Init(this);
                bullet.SetActive(false);
                bullet.transform.SetParent(_bulletContainer.transform);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}