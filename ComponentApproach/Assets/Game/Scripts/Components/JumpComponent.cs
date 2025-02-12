using UnityEngine;

namespace Game.Scripts.Components
{
    public class JumpComponent
    {
        private readonly ICondition _condition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _jumpForce;
        private readonly float _cooldown;

        private float? _lastJumpTime;

        public JumpComponent(
            ICondition condition,
            Rigidbody2D rigidbody, 
            float jumpForce,
            float cooldown)
        {
            _condition = condition;
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
            _cooldown = cooldown;
        }
        
        public void Jump()
        {
            if (_condition.Invoke() && CanJump())
            {
                var zeroVelocityY = _rigidbody.velocity;
                zeroVelocityY.y = 0;
                _rigidbody.velocity = zeroVelocityY;

                if (_rigidbody.isKinematic)
                {
                    _rigidbody.isKinematic = false;
                }
                
                _rigidbody.AddForce(Vector2.up * _jumpForce);
                _lastJumpTime = Time.time;
            }
        }

        private bool CanJump() => 
            !_lastJumpTime.HasValue || Time.time - _lastJumpTime.Value >= _cooldown;
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}