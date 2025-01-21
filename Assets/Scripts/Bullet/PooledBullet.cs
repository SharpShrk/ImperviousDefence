using UnityEngine;

namespace Bullets
{
    public abstract class PooledBullet<TPool, TSelf> : Bullet, IBullet
        where TPool : BulletPool<TSelf>
        where TSelf : Bullet, IBullet
    {
        protected TPool _bulletPool;

        public void Init<TPoolType>(BulletPool<TPoolType> pool) where TPoolType : Component, IBullet
        {
            _bulletPool = pool as TPool;
        }

        protected override void ReturnToPool()
        {
            _bulletPool.ReturnBullet(this as TSelf);
        }
    }
}