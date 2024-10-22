using System.Collections.Generic;
using Common;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour
    {
        private const int StartPoolSize = 10;
        
        [SerializeField] private BulletConstructor _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private Transform _container;

        private readonly List<BulletConstructor> _cache = new(16);
        
        private Pool<BulletConstructor> _pool;

        private void Awake()
        {
            _pool = new Pool<BulletConstructor>(_prefab, _container);
            _pool.Prepare(StartPoolSize);
        }

        private void FixedUpdate()
        {
            CheckBulletBounds();
            ClearCache();
        }

        public void SpawnBullet(
            Vector2 position,
            Color color,
            int physicsLayer,
            int damage,
            Vector2 velocity
        )
        {
            var bullet = _pool.Spawn(_worldTransform);
            bullet.Construct(position, color, physicsLayer, damage, velocity, this);
        }
        
        public void DespawnBullet(BulletConstructor bullet) => 
            _pool.Despawn(bullet);

        private void CheckBulletBounds()
        {
            foreach (var instance in _pool.ActiveInstances)
            {
                if (!_levelBounds.InBounds(instance.Position))
                {
                    _cache.Add(instance);
                }
            }
        }

        private void ClearCache()
        {
            if (_cache.Count > 0)
            {
                foreach (var instance in _cache)
                {
                    DespawnBullet(instance);
                }
                _cache.Clear();
            }
        }
    }
}