using System.Collections.Generic;

namespace Bullets
{
    public class BulletTurretPool : BulletPool<BulletTurret>
    {
        protected override void InitializePool()
        {
            QueueBulletPool = new Queue<BulletTurret>();

            for (int i = 0; i < InitialPoolSize; i++)
            {
                BulletTurret bullet = Instantiate(BulletPrefab);
                bullet.Init(this);

                bullet.gameObject.SetActive(false);
                bullet.transform.SetParent(BulletContainer.transform);
                QueueBulletPool.Enqueue(bullet);
            }
        }
    }
}