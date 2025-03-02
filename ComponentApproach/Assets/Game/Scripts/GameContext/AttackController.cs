using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Content.Collision;
using Game.Scripts.GameObjects.Content.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class AttackController : IInitializable, IDisposable
    {
        private readonly HashSet<IDamageable> _damageables = new();
        private readonly IAttackable _attackable;
        private readonly CollisionEventReceiver _collisionEventReceiver;

        public AttackController(
            IAttackable attackable,
            CollisionEventReceiver collisionEventReceiver)
        {
            _attackable = attackable;
            _collisionEventReceiver = collisionEventReceiver;
        }

        void IInitializable.Initialize()
        {
            _collisionEventReceiver.OnCollisionEnter += OnCollisionEnter2D;
            _collisionEventReceiver.OnCollisionExit += OnCollisionExit2D;
        }

        void IDisposable.Dispose()
        {
            _collisionEventReceiver.OnCollisionEnter -= OnCollisionEnter2D;
            _collisionEventReceiver.OnCollisionExit -= OnCollisionExit2D;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable) &&
                other.gameObject.TryGetComponent(out Rigidbody2D rb) &&
                !_damageables.Contains(damageable))
            {
                _damageables.Add(damageable);
                _attackable.Attack(damageable, rb);
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                _damageables.Remove(damageable);
            }
        }
    }
}