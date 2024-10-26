using System;
using Bullets.Factory;
using Common;
using UnityEngine;

namespace Characters.Common
{
    [Serializable]
    public abstract class ShootingComponent
    {
        [SerializeField] protected Transform _firePoint;
        [SerializeField] private PhysicsLayer _layer;
        [SerializeField] private int _damage;
        [SerializeField] private Color _bulletColor;

        protected abstract BulletFactory BulletFactory { get; }
        
        public virtual void Shoot()
        {
            BulletFactory.SpawnBullet(
                _firePoint.position,
                _bulletColor,
                (int) _layer,
                _damage,
                GetShootDirection()
            );
        }

        protected abstract Vector3 GetShootDirection();
    }
}