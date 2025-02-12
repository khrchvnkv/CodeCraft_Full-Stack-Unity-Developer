using System;
using System.Collections.Generic;
using Game.Scripts.Contracts;
using UnityEngine;

namespace Game.Scripts.Collision
{
    public class AttackCollision : MonoBehaviour
    {
        private readonly HashSet<IDamageable> _damageables = new();

        public event Action<IDamageable, Rigidbody2D> DamageableCollided;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable) &&
                other.gameObject.TryGetComponent(out Rigidbody2D rb) &&
                !_damageables.Contains(damageable))
            {
                _damageables.Add(damageable);
                DamageableCollided?.Invoke(damageable, rb);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                _damageables.Remove(damageable);
            }
        }
    }
}