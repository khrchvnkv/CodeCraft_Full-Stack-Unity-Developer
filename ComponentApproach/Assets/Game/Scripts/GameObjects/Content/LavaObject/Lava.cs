using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.LavaObject
{
    public class Lava : IInitializable, IDisposable
    {
        private readonly TriggerEventReceiver _triggerEventReceiver;
        private readonly AudioSource _audioSource;

        public Lava(
            TriggerEventReceiver triggerEventReceiver,
            AudioSource audioSource)
        {
            _triggerEventReceiver = triggerEventReceiver;
            _audioSource = audioSource;
        }

        void IInitializable.Initialize() => _triggerEventReceiver.OnTriggerEnter += OnTriggerEnter;

        void IDisposable.Dispose() => _triggerEventReceiver.OnTriggerEnter -= OnTriggerEnter;

        private void OnTriggerEnter(Entity entity)
        {
            if (entity.TryGet(out HealthComponent healthComponent))
            {
                healthComponent.Kill();
                _audioSource.Play();
            } 
            else if (entity.TryGet(out DestroyableComponent destroyableComponent))
            {
                destroyableComponent.Destroy();
                _audioSource.Play();
            }
        }
    }
}