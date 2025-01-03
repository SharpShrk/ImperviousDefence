namespace Bullets
{
    public class BulletTurret : Bullet
    {
        private BulletTurretPool _bulletPool;

        public void Init(BulletTurretPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        protected override void ReturnToPool()
        {
            _bulletPool.ReturnBullet(this);
        }
    }
}