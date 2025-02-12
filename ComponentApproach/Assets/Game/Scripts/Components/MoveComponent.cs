using UnityEngine;

namespace Game.Scripts.Components
{
    public class MoveComponent
    {
        private readonly ICondition _condition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _movementSpeed;

        public MoveComponent(
            ICondition condition,
            Rigidbody2D rigidbody, 
            float movementSpeed)
        {
            _condition = condition;
            _rigidbody = rigidbody;
            _movementSpeed = movementSpeed;
        }

        public void Move(in Vector2 direction)
        {
            if (_condition.Invoke())
            {
                var deltaTime = Time.deltaTime;
                var deltaMovement= direction.normalized * _movementSpeed * deltaTime;
                
                {
                    var velocityDeltaMove = _rigidbody.velocity * deltaTime;
                    _rigidbody.position += velocityDeltaMove + deltaMovement;
                }
            }
        }
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}