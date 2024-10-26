using System.Collections.Generic;
using Bullets.Factory;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletCheckBoundsManager : MonoBehaviour
    {
        [SerializeField] private BulletFactory _bulletFactory;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly List<Bullet> _instances = new(32);
        private readonly List<Bullet> _cache = new(16);

        private void OnEnable()
        {
            _bulletFactory.OnSpawned += AddBullet;
            _bulletFactory.OnDespawned += RemoveBullet;
        }

        private void OnDisable()
        {
            _bulletFactory.OnSpawned -= AddBullet;
            _bulletFactory.OnDespawned -= RemoveBullet;
        }

        private void FixedUpdate()
        {
            CheckBulletBounds();
            ClearCache();
        }

        private void AddBullet(Bullet bullet) => _instances.Add(bullet);

        private void RemoveBullet(Bullet bullet) => _instances.Remove(bullet);

        private void CheckBulletBounds()
        {
            foreach (var instance in _instances)
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
                    _bulletFactory.DespawnBullet(instance);
                }
                _cache.Clear();
            }
        }
    }
}