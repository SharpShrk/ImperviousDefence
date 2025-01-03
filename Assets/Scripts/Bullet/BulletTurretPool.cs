using System.Collections.Generic;

namespace Bullets
{
    public class BulletTurretPool : BulletPool<BulletTurret>
    {
        protected override void InitializePool()
        {
            _bulletPool = new Queue<BulletTurret>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                BulletTurret bullet = Instantiate(_bulletPrefab);
                bullet.Init(this);

                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(_bulletContainer.transform);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}