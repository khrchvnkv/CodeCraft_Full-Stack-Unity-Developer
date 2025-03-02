using System;
using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class MoveComponent
    {
        private readonly ICondition _condition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _movementSpeed;

        public event Action<Vector2> MovedInDirection; 

        public MoveComponent(
            ICondition condition,
            Rigidbody2D rigidbody, 
            float movementSpeed)
        {
            _condition = condition;
            _rigidbody = rigidbody;
            _movementSpeed = movementSpeed;
        }

        public void Move(Vector2 direction)
        {
            if (_condition.Invoke())
            {
                var deltaTime = Time.deltaTime;
                direction = direction.normalized;
                var deltaMovement= direction * _movementSpeed * deltaTime;
                deltaMovement.x += _rigidbody.velocity.x * deltaTime;
                _rigidbody.position += deltaMovement;

                MovedInDirection?.Invoke(direction);
            }
        }
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}