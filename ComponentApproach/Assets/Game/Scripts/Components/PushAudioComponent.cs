using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class PushAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;

        private PushComponent _pushComponent;

        [Inject]
        private void Construct(PushComponent pushComponent)
        {
            _pushComponent = pushComponent;
        }

        private void OnEnable()
        {
            _pushComponent.Pushed += UpdateView;
            _pushComponent.EmptyPushed += UpdateView;
        }

        private void OnDisable()
        {
            _pushComponent.Pushed -= UpdateView;
            _pushComponent.EmptyPushed -= UpdateView;
        }

        private void UpdateView() => _audio.Play();
    }
}