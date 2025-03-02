using System;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Push
{
    public class PushAudioComponent : IInitializable, IDisposable
    {
        private readonly PushComponent _pushComponent;
        private readonly AudioSource _audio;
        
        public PushAudioComponent(
            PushComponent pushComponent,
            AudioSource audio)
        {
            _pushComponent = pushComponent;
            _audio = audio;
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

        private void UpdateView() => _audio.Play();

        
    }
}