using Bullets.Factory;
using Characters.Common;
using Characters.EnemyLogic.Factory;
using Characters.PlayerLogic;
using UnityEngine;

namespace Characters.EnemyLogic
{
    public sealed class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private EnemyMovement _movement;
        [SerializeField] private EnemyShooting _shooting;
        [SerializeField] private HealthComponent _healthComponent;

        private IEnemyDespawnCallback _despawnCallback;

        public void Construct(
            in BulletFactory bulletFactory,
            in Player playerShooting, 
            in Vector2 startPoint,
            in Vector2 endPoint, 
            in IEnemyDespawnCallback despawnCallback)
        {
            _movement.Construct(startPoint, endPoint);
            _shooting.Construct(bulletFactory, playerShooting);
            _healthComponent.ResetHealth();
            _despawnCallback = despawnCallback;
            
            _healthComponent.OnHealthEmpty += Died;
        }
        
        public void FixedUpdate()
        {
            if (!_movement.TryMove())
            {
                _shooting.Shoot();
            }
        }

        private void Died()
        {
            _healthComponent.OnHealthEmpty -= Died;
            _despawnCallback?.Destroy(this);
        }

        void IDamageable.DealDamage(in int damage) => 
            _healthComponent.DealDamage(damage);
    }
}