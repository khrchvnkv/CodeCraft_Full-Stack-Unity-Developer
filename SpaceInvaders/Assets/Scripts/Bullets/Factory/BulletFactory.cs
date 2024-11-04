using System;
using Common;
using UnityEngine;

namespace Bullets.Factory
{
    public class BulletFactory : MonoBehaviour, IBulletFactory
    {
        private const int StartPoolSize = 10;

        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        
        private Pool<Bullet> _pool;

        public event Action<Bullet> OnSpawned;
        public event Action<Bullet> OnDespawned;
        
        private void Awake()
        {
            _pool = new Pool<Bullet>(_prefab, _container);
            _pool.Prepare(StartPoolSize);
        }
        
        public Bullet SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        )
        {
            var bullet = _pool.Spawn(_worldTransform);
            bullet.Construct(position, color, physicsLayer, damage, velocity, this);
            OnSpawned?.Invoke(bullet);
            return bullet;
        }
        
        public void DespawnBullet(in Bullet bullet)
        {
            _pool.Despawn(bullet);
            OnDespawned?.Invoke(bullet);
        }

        void IBulletDestroyCallback.Destroy(in Bullet bullet) => DespawnBullet(bullet);
    }
}