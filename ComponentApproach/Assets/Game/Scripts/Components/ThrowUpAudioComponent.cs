using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class ThrowUpAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

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

        private void UpdateView() => _audio.Play();
    }
}