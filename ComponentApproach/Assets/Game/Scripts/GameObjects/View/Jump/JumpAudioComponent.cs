using System;
using DG.Tweening;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.View.Jump
{
    public class JumpAudioComponent : IInitializable, IDisposable
    {
        private readonly JumpComponent _jumpComponent;
        private readonly AudioSource _jumpAudio;

        private Sequence _sequence;

        public JumpAudioComponent(
            JumpComponent jumpComponent,
            AudioSource jumpAudio)
        {
            _jumpComponent = jumpComponent;
            _jumpAudio = jumpAudio;
        }

        void IInitializable.Initialize() => _jumpComponent.Jumped += UpdateJumpView;

        void IDisposable.Dispose() => _jumpComponent.Jumped -= UpdateJumpView;

        private void UpdateJumpView() => _jumpAudio.Play();
    }
}