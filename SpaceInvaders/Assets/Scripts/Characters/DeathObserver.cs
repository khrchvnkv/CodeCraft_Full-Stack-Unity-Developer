using Characters.Common;
using UnityEngine;

namespace Characters
{
    public abstract class DeathObserver : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        
        private void OnEnable() => _healthComponent.OnHealthEmpty += Died;

        private void OnDisable() => _healthComponent.OnHealthEmpty += Died;

        protected abstract void Died();
    }
}