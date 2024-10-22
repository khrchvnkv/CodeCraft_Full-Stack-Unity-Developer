using System;
using UnityEngine;

namespace Characters
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private int _health;

        private int _currentHealth;
        
        public bool IsAlive => _currentHealth > 0;

        public event Action<int> OnHealthChanged;
        public event Action OnHealthEmpty;

        public void ResetHealth() => _currentHealth = _health;

        public void DealDamage(in int damage)
        {
            if (!IsAlive) return;

            _currentHealth = Mathf.Max(0, _currentHealth - damage);
            OnHealthChanged?.Invoke(_health);

            if (!IsAlive)
            {
                OnHealthEmpty?.Invoke();
            }
        }
    }
}