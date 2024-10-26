using System;
using UnityEngine;

namespace Bullets
{
    [Serializable]
    public class BulletVelocity
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        public void SetVelocity(in Vector2 velocity) => _rigidbody2D.velocity = velocity;
    }
}