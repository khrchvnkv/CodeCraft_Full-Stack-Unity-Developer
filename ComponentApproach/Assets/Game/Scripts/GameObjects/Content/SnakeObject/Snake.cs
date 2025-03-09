using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SnakeObject
{
    public class Snake : 
        IInitializable,
        IFixedTickable,
        IDisposable
    {
        private readonly AttackComponent _attackComponent;
        private readonly CollisionEventReceiver _collisionEventReceiver;
        private readonly TossComponent _tossComponent;
        private readonly MoveComponent _moveComponent;
        private readonly RotateComponent _rotateComponent;
        private readonly WaypointMovementComponent _waypointMovementComponent;

        public Snake(
            AttackComponent attackComponent,
            CollisionEventReceiver collisionEventReceiver,
            TossComponent tossComponent,
            MoveComponent moveComponent,
            RotateComponent rotateComponent,
            WaypointMovementComponent waypointMovementComponent)
        {
            _attackComponent = attackComponent;
            _collisionEventReceiver = collisionEventReceiver;
            _tossComponent = tossComponent;
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
            _waypointMovementComponent = waypointMovementComponent;
        }
        
        void IInitializable.Initialize()
        {
            _collisionEventReceiver.OnCollisionEnter += OnCollisionEnter;
            _moveComponent.MovedInDirection += UpdateRotation;
        }

        private void OnCollisionEnter(Entity entity)
        {
            _attackComponent.Attack(entity);
            _tossComponent.Toss(entity);
        }
        
        private void UpdateRotation(Vector2 direction) => _rotateComponent.LookInDirection(direction);

        void IFixedTickable.FixedTick() => _waypointMovementComponent.Move();

        void IDisposable.Dispose()
        {
            _collisionEventReceiver.OnCollisionEnter -= OnCollisionEnter;
            _moveComponent.MovedInDirection -= UpdateRotation;
        }
    }
}