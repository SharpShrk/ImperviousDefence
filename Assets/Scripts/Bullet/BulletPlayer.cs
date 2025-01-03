namespace Bullets
{
    public class BulletPlayer : Bullet
    {
        private BulletPlayerPool _bulletPool;

        public void Init(BulletPlayerPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        protected override void ReturnToPool()
        {
            _bulletPool.ReturnBullet(this);
        }
    }
}