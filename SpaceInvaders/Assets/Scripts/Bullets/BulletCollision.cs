using System;
using Characters;
using UnityEngine;

namespace Bullets
{
    public class BulletCollision : MonoBehaviour
    {
        private int _damage;
        
        public event Action Collided;

        public void SetDamage(in int damage) => _damage = damage;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.DealDamage(_damage);
                Collided?.Invoke();
            }
        }
    }
}