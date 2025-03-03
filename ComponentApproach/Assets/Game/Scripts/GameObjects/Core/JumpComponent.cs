using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Core
{
    public class JumpComponent : IFixedTickable
    {
        private readonly ICondition _condition;
        private readonly Rigidbody2D _rigidbody;
        private readonly float _jumpForce;
        private readonly float _cooldown;

        private float? _lastJumpTime;
        private bool _jumpRequested;

        public event Action Jumped; 

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

        void IFixedTickable.FixedTick()
        {
            if (_jumpRequested)
            {
                Jump();
                _jumpRequested = false;
            }
        }

        public void RequestJump() => _jumpRequested = true;

        private void Jump()
        {
            if (_condition.Invoke() && CanJump())
            {
                var zeroVelocityY = _rigidbody.velocity;
                zeroVelocityY.y = 0;
                _rigidbody.velocity = zeroVelocityY;
                _rigidbody.AddForce(Vector2.up * _jumpForce);
                _lastJumpTime = Time.time;
                
                Jumped?.Invoke();
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