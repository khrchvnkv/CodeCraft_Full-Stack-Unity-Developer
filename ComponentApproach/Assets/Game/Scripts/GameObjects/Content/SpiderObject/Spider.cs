using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SpiderObject
{
    public class Spider :
        IInitializable,
        IDisposable
    {
        private readonly AttackComponent _attackComponent;
        private readonly CollisionEventReceiver _collisionEventReceiver;
        private readonly PushComponent _pushComponent;
        private readonly Rigidbody2D _rigidbody;

        public Spider(
            AttackComponent attackComponent,
            CollisionEventReceiver collisionEventReceiver,
            PushComponent pushComponent,
            Rigidbody2D rigidbody)
        {
            _attackComponent = attackComponent;
            _collisionEventReceiver = collisionEventReceiver;
            _pushComponent = pushComponent;
            _rigidbody = rigidbody;
        }
        
        void IInitializable.Initialize() => _collisionEventReceiver.OnCollisionEnter += OnCollisionEnter;

        private void OnCollisionEnter(Entity entity)
        {
            _attackComponent.Attack(entity);
            if (entity.TryGet(out Rigidbody2D rigidbody))
            {
                var direction = rigidbody.position - _rigidbody.position;
                _pushComponent.Push(rigidbody, direction);
            }
        }

        void IDisposable.Dispose() => _collisionEventReceiver.OnCollisionEnter -= OnCollisionEnter;
    }
}