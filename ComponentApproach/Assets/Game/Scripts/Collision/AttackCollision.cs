using System.Collections.Generic;
using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Collision
{
    public class AttackCollision : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        
        private readonly HashSet<IDamageable> _damageables = new();

        private IAttackable _attackable;
        
        [Inject]
        private void Construct(IAttackable attackable)
        {
            _attackable = attackable;
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable) &&
                other.gameObject.TryGetComponent(out Rigidbody2D rb) &&
                !_damageables.Contains(damageable))
            {
                _damageables.Add(damageable);
                _attackable.Attack(damageable, rb);
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