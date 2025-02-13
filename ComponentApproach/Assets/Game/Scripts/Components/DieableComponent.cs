using Game.Scripts.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class DieableComponent : MonoBehaviour,
        IDieable
    {
        private HealthComponent _healthComponent;
        
        [Inject]
        private void Construct(HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }
        
        void IDieable.Die() => _healthComponent.Kill();
    }
}