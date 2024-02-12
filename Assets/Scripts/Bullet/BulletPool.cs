using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public abstract class BulletPool : MonoBehaviour
    {
        [SerializeField] protected GameObject _bulletPrefab;
        [SerializeField] protected int _initialPoolSize = 10;
        [SerializeField] protected GameObject _bulletContainer;

        protected Queue<GameObject> _bulletPool;

        protected void Awake()
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

        protected abstract void InitializePool();        
    }
}