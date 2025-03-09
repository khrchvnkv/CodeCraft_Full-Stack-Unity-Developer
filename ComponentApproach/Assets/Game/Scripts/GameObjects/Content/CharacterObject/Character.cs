using System;
using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.CharacterObject
{
    public sealed class Character : IInitializable, IFixedTickable, IDisposable
    {
        private readonly TriggerDetector _triggerDetector;
        private readonly PushComponent _pushComponent;
        private readonly TossComponent _tossComponent;
        private readonly MoveComponent _moveComponent;
        private readonly RotateComponent _rotateComponent;

        private bool _isPushRequested;
        private bool _isTossRequested;

        public Character(
            TriggerDetector triggerDetector,
            PushComponent pushComponent,
            TossComponent tossComponent,
            MoveComponent moveComponent,
            RotateComponent rotateComponent)
        {
            _triggerDetector = triggerDetector;
            _pushComponent = pushComponent;
            _tossComponent = tossComponent;
            _moveComponent = moveComponent;
            _rotateComponent = rotateComponent;
        }

        public void RequestPush() => _isPushRequested = true;
        public void RequestToss() => _isTossRequested = true;

        void IInitializable.Initialize() => _moveComponent.MovedInDirection += UpdateRotation;

        void IFixedTickable.FixedTick()
        {
            if (_isPushRequested)
            {
                Push();
                _isPushRequested = false;
            }

            if (_isTossRequested)
            {
                Toss();
                _isTossRequested = false;
            }
        }
        
        void IDisposable.Dispose() => _moveComponent.MovedInDirection -= UpdateRotation;

        private void UpdateRotation(Vector2 direction) => _rotateComponent.LookInDirection(direction);

        private void Push() => _pushComponent.Push(_triggerDetector.Targets);

        private void Toss() => _tossComponent.Toss(_triggerDetector.Targets);
    }
}