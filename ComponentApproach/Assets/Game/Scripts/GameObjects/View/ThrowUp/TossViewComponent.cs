using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.ThrowUp
{
    public class TossViewComponent : IInitializable, IDisposable
    {
        private readonly TossComponent _tossComponent;
        private readonly ParticleSystem _particle;

        public TossViewComponent(
            TossComponent tossComponent,
            ParticleSystem particle)
        {
            _tossComponent = tossComponent;
            _particle = particle;
        }

        void IInitializable.Initialize()
        {
            _tossComponent.Tossed += UpdateView;
            _tossComponent.EmptyTossed += UpdateView;
        }

        void IDisposable.Dispose()
        {
            _tossComponent.Tossed -= UpdateView;
            _tossComponent.EmptyTossed -= UpdateView;
        }

        private void UpdateView() => _particle.Play();
    }
}