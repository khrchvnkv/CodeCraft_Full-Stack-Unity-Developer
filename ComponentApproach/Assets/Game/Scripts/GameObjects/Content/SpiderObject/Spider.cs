using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SpiderObject
{
    public class Spider :
        IInitializable,
        IFixedTickable,
        IDisposable
    {
        private readonly AttackComponent _attackComponent;
        private readonly CollisionEventReceiver _collisionEventReceiver;
        private readonly PushComponent _pushComponent;
        private readonly Rigidbody2D _rigidbody;
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public Spider(
            AttackComponent attackComponent,
            CollisionEventReceiver collisionEventReceiver,
            PushComponent pushComponent,
            Rigidbody2D rigidbody,
            WaypointMovementComponent waypointMovementComponent)
        {
            _attackComponent = attackComponent;
            _collisionEventReceiver = collisionEventReceiver;
            _pushComponent = pushComponent;
            _rigidbody = rigidbody;
            _waypointMovementComponent = waypointMovementComponent;
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
        
        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();

        void IDisposable.Dispose() => _collisionEventReceiver.OnCollisionEnter -= OnCollisionEnter;
    }
}