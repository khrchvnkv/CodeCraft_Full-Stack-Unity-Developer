using System;
using Game.Scripts.GameObjects.Content.Contracts;
using Game.Scripts.GameObjects.Content.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class KillZoneController : IInitializable, IDisposable
    {
        private readonly TriggerEventReceiver _receiver;
        private readonly AudioSource _audioSource;

        public KillZoneController(
            TriggerEventReceiver receiver, 
            AudioSource audioSource)
        {
            _receiver = receiver;
            _audioSource = audioSource;
        }

        void IInitializable.Initialize() => _receiver.OnTriggerEnter += OnTriggerEnter2D;

        void IDisposable.Dispose() => _receiver.OnTriggerEnter -= OnTriggerEnter2D;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDieable dieable))
            {
                dieable.Die();
                _audioSource.Play();
            }
        }
    }
}