using UnityEngine;

namespace Bullets.Factory
{
    public interface IBulletFactory : IBulletDestroyCallback
    {
        Bullet SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        );
    }
}