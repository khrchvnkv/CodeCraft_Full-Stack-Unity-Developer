using System;
using Bullets.Factory;
using Characters.Common;
using UnityEngine;

namespace Characters.PlayerLogic
{
    [Serializable]
    public class PlayerShooting : ShootingComponent
    {
        [SerializeField] private BulletFactory _bulletFactory;
        
        protected override BulletFactory BulletFactory => _bulletFactory;
        
        protected override Vector3 GetShootDirection() => 
            _firePoint.rotation * Vector3.up * 3;
    }
}