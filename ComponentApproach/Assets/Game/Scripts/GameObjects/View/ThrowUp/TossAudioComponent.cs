using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.ThrowUp
{
    public class TossAudioComponent : IInitializable, IDisposable
    {
        private readonly TossComponent _tossComponent;
        private readonly AudioSource _audio;

        public TossAudioComponent(
            TossComponent tossComponent,
            AudioSource audio)
        {
            _tossComponent = tossComponent;
            _audio = audio;
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

        private void UpdateView() => _audio.Play();
    }
}