using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class ThrowUpViewComponent : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;

        private ThrowUpComponent _throwUpComponent;

        [Inject]
        private void Construct(ThrowUpComponent throwUpComponent)
        {
            _throwUpComponent = throwUpComponent;
        }

        private void OnEnable()
        {
            _throwUpComponent.ThrowedUp += UpdateView;
            _throwUpComponent.EmptyThrowed += UpdateView;
        }

        private void OnDisable()
        {
            _throwUpComponent.ThrowedUp -= UpdateView;
            _throwUpComponent.EmptyThrowed -= UpdateView;
        }

        private void UpdateView() => _particle.Play();
    }
}