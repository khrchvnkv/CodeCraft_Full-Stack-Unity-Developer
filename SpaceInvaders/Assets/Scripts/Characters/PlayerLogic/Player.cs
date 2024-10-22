using System;
using Bullets;
using Common;
using UnityEngine;

namespace Characters.PlayerLogic
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _health;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private BulletManager _bulletManager;

        public bool IsAlive => _health > 0;
        public Vector2 Position => _rigidbody.position;
        
        public event Action<Player, int> OnHealthChanged;
        public event Action<Player> OnHealthEmpty;

        public void Move(Vector2 direction)
        {
            Vector2 moveStep = direction * (_speed * Time.fixedDeltaTime);
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }

        public void DealDamage(in int damage)
        {
            if (!IsAlive) return;

            _health = Mathf.Max(0, _health - damage);
            OnHealthChanged?.Invoke(this, _health);

            if (!IsAlive)
            {
                OnHealthEmpty?.Invoke(this);
            }
        }

        public void SpawnBullet()
        {
            _bulletManager.SpawnBullet(
                _firePoint.position,
                Color.blue,
                (int) PhysicsLayer.PLAYER_BULLET,
                1,
                _firePoint.rotation * Vector3.up * 3
            );
        }
    }
}