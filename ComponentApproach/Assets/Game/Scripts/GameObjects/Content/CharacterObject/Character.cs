using System;
using System.Collections.Generic;
using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.CharacterObject
{
    public sealed class Character : IFixedTickable
    {
        private readonly TriggerDetector _triggerDetector;
        private readonly PushComponent _pushComponent;
        private readonly TossComponent _tossComponent;
        private readonly Rigidbody2D _rigidbody;

        private bool _isPushRequested;
        private bool _isTossRequested;

        public Character(
            TriggerDetector triggerDetector,
            PushComponent pushComponent,
            TossComponent tossComponent,
            Rigidbody2D rigidbody)
        {
            _triggerDetector = triggerDetector;
            _pushComponent = pushComponent;
            _tossComponent = tossComponent;
            _rigidbody = rigidbody;
        }

        public void RequestPush() => _isPushRequested = true;
        public void RequestToss() => _isTossRequested = true;

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

        private void Push()
        {
            var collection = _triggerDetector.Targets;
            if (collection.Count == 0)
            {
                _pushComponent.Push(Array.Empty<KeyValuePair<Rigidbody2D, Vector2>>());
                return;
            }

            KeyValuePair<Rigidbody2D, Vector2>[] pushData = new KeyValuePair<Rigidbody2D, Vector2>[collection.Count];
            var index = 0;
            foreach (var body in collection)
            {
                var direction = body.position - _rigidbody.position;
                pushData[index] = new KeyValuePair<Rigidbody2D, Vector2>(body, direction);
                index++;
            }
            
            _pushComponent.Push(pushData);
        }
        
        private void Toss()
        {
            var targets = _triggerDetector.Targets;
            _tossComponent.Toss(targets);
        }
    }
}