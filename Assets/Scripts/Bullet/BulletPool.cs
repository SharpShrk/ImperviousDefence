using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public abstract class BulletPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected T _bulletPrefab;
        [SerializeField] protected int _initialPoolSize = 10;
        [SerializeField] protected GameObject _bulletContainer;

        protected Queue<T> _bulletPool;

        protected void Awake()
        {
            InitializePool();
        }

        public T GetBullet()
        {
            if (_bulletPool.Count > 0)
            {
                T bullet = _bulletPool.Dequeue();
                bullet.gameObject.SetActive(true);
                bullet.transform.SetParent(_bulletContainer.transform);

                return bullet;
            }

            T newBullet = Instantiate(_bulletPrefab);
            return newBullet;
        }

        public void ReturnBullet(T bullet)
        {
            bullet.gameObject.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }

        protected abstract void InitializePool();
    }
}