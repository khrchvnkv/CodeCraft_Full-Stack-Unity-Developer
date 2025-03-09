using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Core
{
    public class MoveComponent : IFixedTickable
    {
        private readonly ICondition _condition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _movementSpeed;

        public event Action<Vector2> MovedInDirection;

        private Vector2 _direction;

        public MoveComponent(
            [InjectOptional] ICondition condition,
            Rigidbody2D rigidbody, 
            float movementSpeed)
        {
            _condition = condition;
            _rigidbody = rigidbody;
            _movementSpeed = movementSpeed;
        }

        public void SetDirection(in Vector2 direction) => _direction = direction;

        public void Move(Vector2 direction)
        {
            if (_condition != null && !_condition.Invoke()) return;

            var deltaTime = Time.deltaTime;
            direction = direction.normalized;
            var deltaMovement= direction * _movementSpeed * deltaTime;
            deltaMovement.x += _rigidbody.velocity.x * deltaTime;
            _rigidbody.position += deltaMovement;

            MovedInDirection?.Invoke(direction);
        }

        void IFixedTickable.FixedTick() => Move(_direction);

        public interface ICondition
        {
            bool Invoke();
        }
    }
}