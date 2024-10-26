using System;
using Characters;
using Characters.Common;
using UnityEngine;

namespace Bullets
{
    [Serializable]
    public class BulletCollision
    {
        private int _damage;
        
        public event Action Collided;

        public void SetDamage(in int damage) => _damage = damage;
        
        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.DealDamage(_damage);
                Collided?.Invoke();
            }
        }
    }
}