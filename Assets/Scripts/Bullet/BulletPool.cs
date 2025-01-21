using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public abstract class BulletPool<T> : MonoBehaviour where T : Component, IBullet
    {
        [SerializeField] protected T BulletPrefab;
        [SerializeField] protected int InitialPoolSize = 10;
        [SerializeField] protected GameObject BulletContainer;

        protected Queue<T> QueueBulletPool;

        protected void Awake()
        {
            InitializePool();
        }

        public T GetBullet()
        {
            if (QueueBulletPool.Count > 0)
            {
                T bullet = QueueBulletPool.Dequeue();
                bullet.gameObject.SetActive(true);
                bullet.transform.SetParent(BulletContainer.transform);

                return bullet;
            }

            T newBullet = Instantiate(BulletPrefab);
            newBullet.Init(this);
            return newBullet;
        }

        public void ReturnBullet(T bullet)
        {
            bullet.gameObject.SetActive(false);
            QueueBulletPool.Enqueue(bullet);
        }

        protected virtual void InitializePool()
        {
            QueueBulletPool = new Queue<T>();

            for (int i = 0; i < InitialPoolSize; i++)
            {
                T bullet = Instantiate(BulletPrefab);
                bullet.Init(this);
                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(BulletContainer.transform);
                QueueBulletPool.Enqueue(bullet);
            }
        }
    }
}