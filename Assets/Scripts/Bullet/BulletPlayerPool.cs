using System.Collections.Generic;
using UnityEngine;

namespace Bullets
{
    public class BulletPlayerPool : BulletPool
    {
        protected override void InitializePool()
        {
            _bulletPool = new Queue<GameObject>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletPrefab);
                bullet.GetComponent<BulletPlayer>().Init(this);

                bullet.SetActive(false);
                bullet.transform.SetParent(_bulletContainer.transform);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}