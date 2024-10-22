using Bullets;
using Characters.PlayerLogic;
using Common;
using UnityEngine;

namespace Characters.EnemyLogic
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _countdown;

        private BulletManager _bulletManager;
        private PlayerConstructor _target;
        private float _currentTime;

        private bool IsTimerExpired => _currentTime <= 0;

        public void Construct(
            BulletManager bulletManager,
            PlayerConstructor player)
        {
            _bulletManager = bulletManager;
            _target = player;
            ResetTimer();
        }
        
        public void Shoot()
        {
            if (!_target.IsAlive)
                return;

            _currentTime -= Time.fixedDeltaTime;
            if (IsTimerExpired)
            {
                Vector2 startPosition = _firePoint.position;
                Vector2 vector = _target.Position - startPosition;
                Vector2 direction = vector.normalized;
                    
                _bulletManager.SpawnBullet(
                    startPosition,
                    Color.red,
                    (int) PhysicsLayer.ENEMY_BULLET,
                    1,
                    direction * 2
                );
                ResetTimer();
            }
        }
        
        private void ResetTimer() => _currentTime = _countdown;
    }
}