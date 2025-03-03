using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SnakeObject
{
    public class Snake : 
        IInitializable,
        IDisposable
    {
        private readonly AttackComponent _attackComponent;
        private readonly CollisionEventReceiver _collisionEventReceiver;
        private readonly TossComponent _tossComponent;

        public Snake(
            AttackComponent attackComponent,
            CollisionEventReceiver collisionEventReceiver,
            TossComponent tossComponent)
        {
            _attackComponent = attackComponent;
            _collisionEventReceiver = collisionEventReceiver;
            _tossComponent = tossComponent;
        }
        
        void IInitializable.Initialize() => _collisionEventReceiver.OnCollisionEnter += OnCollisionEnter;

        private void OnCollisionEnter(Entity entity)
        {
            _attackComponent.Attack(entity);
            _tossComponent.Toss(entity);
        }

        void IDisposable.Dispose() => _collisionEventReceiver.OnCollisionEnter -= OnCollisionEnter;
    }
}