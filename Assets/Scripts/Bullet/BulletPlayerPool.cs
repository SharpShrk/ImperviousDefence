using System.Collections.Generic;

namespace Bullets
{
    public class BulletPlayerPool : BulletPool<BulletPlayer>
    {
        protected override void InitializePool()
        {
            _bulletPool = new Queue<BulletPlayer>();

            for (int i = 0; i < _initialPoolSize; i++)
            {
                BulletPlayer bullet = Instantiate(_bulletPrefab);
                bullet.Init(this);

                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(_bulletContainer.transform);
                _bulletPool.Enqueue(bullet);
            }
        }
    }
}