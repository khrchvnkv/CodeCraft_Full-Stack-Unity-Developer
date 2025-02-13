using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class DamageableComponent : MonoBehaviour,
        IDamageable
    {
        private HealthComponent _healthComponent;
        
        [Inject]
        private void Construct(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        void IDamageable.TakeDamage(in int damage) => _healthComponent.TakeDamage(damage);
    }
}