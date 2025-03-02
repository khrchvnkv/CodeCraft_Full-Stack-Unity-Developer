using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Push
{
    public class PushViewComponent : IInitializable, IDisposable
    {
        private readonly PushComponent _pushComponent;
        private readonly ParticleSystem _particle;

        public PushViewComponent(
            PushComponent pushComponent,
            ParticleSystem particle)
        {
            _pushComponent = pushComponent;
            _particle = particle;
        }

        void IInitializable.Initialize()
        {
            _pushComponent.Pushed += UpdateView;
            _pushComponent.EmptyPushed += UpdateView;
        }

        void IDisposable.Dispose()
        {
            _pushComponent.Pushed -= UpdateView;
            _pushComponent.EmptyPushed -= UpdateView;
        }

        private void UpdateView() => _particle.Play();
    }
}