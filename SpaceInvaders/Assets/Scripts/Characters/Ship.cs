using System;
using Bullets.Factory;
using Characters.Common;
using UnityEngine;

namespace Characters
{
    public class Ship : MonoBehaviour, IDamageable
    {
        [SerializeField] private ShootingComponent _shooting;
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private HealthComponent _health;
        
        public bool IsAlive => _health.IsAlive;
        public Vector2 Position => _movement.Position;

        public event Action OnDied; 

        private void Awake() => ResetState();

        private void OnEnable() => _health.OnHealthEmpty += Died;

        private void OnDisable() => _health.OnHealthEmpty -= Died;

        public void ResetState() => _health.ResetHealth();

        public void Construct(in IBulletFactory bulletFactory) => 
            _shooting.Construct(bulletFactory);

        public void SetPosition(in Vector2 position) => _movement.SetPosition(position);

        public void Move(in float moveDirectionX)
        {
            Vector2 moveDirection = new Vector2(moveDirectionX, 0);
            Move(moveDirection);
        }

        public void Move(in Vector2 direction) => _movement.Move(direction);

        public void Shoot(in Vector2 direction) => _shooting.Shoot(direction);
        
        private void Died() => OnDied?.Invoke();

        void IDamageable.DealDamage(in int damage) => _health.DealDamage(damage);
    }
}