using System;
using Bullets.Factory;
using Characters.Common;
using Characters.PlayerLogic;
using UnityEngine;

namespace Characters.EnemyLogic
{
    [Serializable]
    public class EnemyShooting : ShootingComponent
    {
        [SerializeField] private float _countdown;

        private BulletFactory _bulletFactory;
        private Player _target;
        private float _currentTime;

        private bool IsTimerExpired => _currentTime <= 0;
        
        protected override BulletFactory BulletFactory => _bulletFactory;

        public void Construct(
            BulletFactory bulletFactory,
            Player player)
        {
            _bulletFactory = bulletFactory;
            _target = player;
            ResetTimer();
        }
        
        public override void Shoot()
        {
            if (!_target.IsAlive)
                return;

            _currentTime -= Time.fixedDeltaTime;
            if (IsTimerExpired)
            {
                base.Shoot();
                ResetTimer();
            }
        }

        protected override Vector3 GetShootDirection()
        {
            Vector2 startPosition = _firePoint.position;
            Vector2 vector = _target.Position - startPosition;
            Vector2 direction = vector.normalized;
            return 2.0f * direction;
        }

        private void ResetTimer() => _currentTime = _countdown;
    }
}