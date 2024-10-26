using System;
using UnityEngine;

namespace Characters.Common
{
    [Serializable]
    public class MovementComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        
        public Vector2 Position => _rigidbody.position;

        public void SetPosition(in Vector2 position) => 
            _rigidbody.MovePosition(position);

        public void Move(in Vector2 direction)
        {
            Vector2 moveStep = direction * (_speed * Time.fixedDeltaTime);
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }
    }
}