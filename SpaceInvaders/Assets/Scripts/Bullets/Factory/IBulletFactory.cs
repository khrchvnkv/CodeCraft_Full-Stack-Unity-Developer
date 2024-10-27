using UnityEngine;

namespace Bullets.Factory
{
    public interface IBulletFactory
    {
        Bullet SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        );

        void DespawnBullet(in Bullet bullet);
    }
}