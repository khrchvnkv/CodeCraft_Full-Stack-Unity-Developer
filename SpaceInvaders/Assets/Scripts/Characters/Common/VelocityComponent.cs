using System;
using UnityEngine;

namespace Characters.Common
{
    [Serializable]
    public class VelocityComponent
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
        
        public void SetVelocity(in Vector2 velocity) => _rigidbody2D.velocity = velocity * _speed;
    }
}