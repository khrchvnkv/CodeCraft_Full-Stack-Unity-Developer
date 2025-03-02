using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.Character
{
    public class CharacterPushController : ITickable, IFixedTickable
    {
        private readonly EntityProvider _entityProvider;
        
        private Rigidbody2D _rigidbody;
        private PushComponent _pushComponent;
        private TriggerAreaController _triggerAreaController;
        private bool _isPushRequest;

        public CharacterPushController(EntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }

        void ITickable.Tick() => ReadPushInput();

        void IFixedTickable.FixedTick() => Push();

        private void ReadPushInput()
        {
            if (Input.GetMouseButtonDown(0) && !_isPushRequest)
            {
                _isPushRequest = true;
            }
        }

        private void Push()
        {
            if (!_isPushRequest)
            {
                return;
            }

            _rigidbody ??= _entityProvider.Value.Get<Rigidbody2D>();
            _pushComponent ??= _entityProvider.Value.Get<PushComponent>();
            _triggerAreaController ??= _entityProvider.Value.Get<TriggerAreaController>();
            
            if (_isPushRequest)
            {
                PushAction();
            }
            _isPushRequest = false;
        }

        private void PushAction()
        {
            var collection = _triggerAreaController.Targets;
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
    }
}