using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Health
{
    public class HealthAudioComponent : IInitializable, IDisposable
    {
        private readonly HealthComponent _healthComponent;
        private readonly AudioSource _takeDamageAudio;

        public HealthAudioComponent(
            HealthComponent healthComponent,
            AudioSource audioSource)
        {
            _healthComponent = healthComponent;
            _takeDamageAudio = audioSource;
        }

        void IInitializable.Initialize()
        {
            _healthComponent.HealthPointsDecreased += PlayDamageAudio;
        }

        void IDisposable.Dispose()
        {
            if (_healthComponent != null)
            {
                _healthComponent.HealthPointsDecreased -= PlayDamageAudio;
            }
        }

        private void PlayDamageAudio() => _takeDamageAudio.Play();
    }
}