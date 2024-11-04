using System;
using Bullets.Factory;
using Common;
using UnityEngine;

namespace Characters.Common
{
    [Serializable]
    public class ShootingComponent
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PhysicsLayer _layer;
        [SerializeField] private int _damage;
        [SerializeField] private Color _bulletColor;

        private IBulletFactory _bulletFactory;
        
        public void Construct(IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void Shoot(Vector2 direction)
        {
            direction = direction.normalized;
            CreateBullet(direction);
        }

        private void CreateBullet(in Vector2 shootingDirection)
        {
            _bulletFactory.SpawnBullet(
                _firePoint.position,
                _bulletColor,
                (int) _layer,
                _damage,
                shootingDirection
            );
        }
    }
}