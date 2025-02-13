using UnityEngine;
using Zenject;

namespace Game.Scripts.Components
{
    public class HealthAudioComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _takeDamageAudio;

        private HealthComponent _healthComponent;

        [Inject]
        private void Construct(
            HealthComponent healthComponent)
        {
            _healthComponent = healthComponent;
        }

        private void OnEnable()
        {
            _healthComponent.HealthPointsDecreased += PlayDamageAudio;
        }

        private void OnDisable()
        {
            if (_healthComponent != null)
            {
                _healthComponent.HealthPointsDecreased -= PlayDamageAudio;
            }
        }

        private void PlayDamageAudio() => _takeDamageAudio.Play();
    }
}