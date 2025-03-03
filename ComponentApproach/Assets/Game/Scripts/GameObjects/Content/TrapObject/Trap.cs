using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.TrapObject
{
    public class Trap :
        IInitializable,
        IDisposable
    {
        private readonly AttackComponent _attackComponent;
        private readonly PushComponent _pushComponent;
        private readonly CollisionEventReceiver _collisionEventReceiver;
        private readonly Rigidbody2D _rigidbody;

        public Trap(
            AttackComponent attackComponent, 
            PushComponent pushComponent, 
            CollisionEventReceiver collisionEventReceiver,
            Rigidbody2D rigidbody)
        {
            _attackComponent = attackComponent;
            _pushComponent = pushComponent;
            _collisionEventReceiver = collisionEventReceiver;
            _rigidbody = rigidbody;
        }

        void IInitializable.Initialize() => _collisionEventReceiver.OnCollisionEnter += OnTriggerEnter;

        private void OnTriggerEnter(Entity entity)
        {
            _attackComponent.Attack(entity);
            if (entity.TryGet(out Rigidbody2D rigidbody))
            {
                var direction = rigidbody.position - _rigidbody.position;
                _pushComponent.Push(rigidbody, direction);
            }
        }

        void IDisposable.Dispose() => _collisionEventReceiver.OnCollisionEnter -= OnTriggerEnter;
    }
}