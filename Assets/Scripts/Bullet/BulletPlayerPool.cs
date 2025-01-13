using System.Collections.Generic;

namespace Bullets
{
    public class BulletPlayerPool : BulletPool<BulletPlayer>
    {
        protected override void InitializePool()
        {
            QueueBulletPool = new Queue<BulletPlayer>();

            for (int i = 0; i < InitialPoolSize; i++)
            {
                BulletPlayer bullet = Instantiate(BulletPrefab);
                bullet.Init(this);

                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(BulletContainer.transform);
                QueueBulletPool.Enqueue(bullet);
            }
        }
    }
}