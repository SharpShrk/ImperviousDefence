using UnityEngine;

namespace Bullets
{
    public interface IBullet
    {
        void Init<TPool>(BulletPool<TPool> pool) where TPool : Component, IBullet;
    }
}