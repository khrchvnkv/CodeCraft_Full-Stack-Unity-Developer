using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Content.Contracts;
using Game.Scripts.GameObjects.Content.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class PlatformMovablesController : IInitializable, IFixedTickable, IDisposable
    {
        private readonly TriggerEventReceiver _receiver;
        private readonly Rigidbody2D _rigidbody2D;
        
        private readonly HashSet<IPlatformMovable> _platformMovables = new();

        private Vector2? _lastPosition;

        public PlatformMovablesController(
            TriggerEventReceiver receiver, 
            Rigidbody2D rigidbody2D)
        {
            _receiver = receiver;
            _rigidbody2D = rigidbody2D;
        }

        void IInitializable.Initialize()
        {
            _receiver.OnTriggerEnter += OnTriggerEnter2D;
            _receiver.OnTriggerExit += OnTriggerExit2D;
        }

        void IFixedTickable.FixedTick()
        {
            if (!_lastPosition.HasValue)
            {
                _lastPosition = _rigidbody2D.position;
                return;
            }

            var delta = _rigidbody2D.position - _lastPosition.Value;

            foreach (var platformMovable in _platformMovables)
            {
                platformMovable.Rigidbody.position += delta;
            }
            _lastPosition = _rigidbody2D.position;
        }

        void IDisposable.Dispose()
        {
            _receiver.OnTriggerEnter -= OnTriggerEnter2D;
            _receiver.OnTriggerExit -= OnTriggerExit2D;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPlatformMovable platformMovable))
            {
                _platformMovables.Add(platformMovable);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out IPlatformMovable platformMovable))
            {
                _platformMovables.Remove(platformMovable);
            }
        }
    }
}