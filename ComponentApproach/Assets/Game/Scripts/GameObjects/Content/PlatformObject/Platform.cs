using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.PlatformObject
{
    public class Platform : 
        IInitializable, 
        IFixedTickable,
        IDisposable
    {
        private readonly TriggerEventReceiver _triggerEventReceiver;
        private readonly Rigidbody2D _rigidbody;
        private readonly WaypointMovementComponent _waypointMovementComponent;
        private readonly HashSet<Rigidbody2D> _movables = new();

        private Vector2? _lastPosition;

        public Platform(
            TriggerEventReceiver triggerEventReceiver, 
            Rigidbody2D rigidbody,
            WaypointMovementComponent waypointMovementComponent)
        {
            _triggerEventReceiver = triggerEventReceiver;
            _rigidbody = rigidbody;
            _waypointMovementComponent = waypointMovementComponent;
        }

        private void OnTriggerEnter(Entity other)
        {
            if (other.TryGet(out Rigidbody2D platformMovable))
            {
                _movables.Add(platformMovable);
            }
        }
        
        private void OnTriggerExit(Entity other)
        {
            if (other.TryGet(out Rigidbody2D platformMovable))
            {
                _movables.Remove(platformMovable);
            }
        }

        void IInitializable.Initialize()
        {
            _triggerEventReceiver.OnTriggerEnter += OnTriggerEnter;
            _triggerEventReceiver.OnTriggerExit += OnTriggerExit;
        }

        void IFixedTickable.FixedTick()
        {
            MoveByWaypoints();
            MovePlatformMovables();
        }

        private void MoveByWaypoints() => _waypointMovementComponent.Move();

        private void MovePlatformMovables()
        {
            if (!_lastPosition.HasValue)
            {
                _lastPosition = _rigidbody.position;
                return;
            }

            var delta = _rigidbody.position - _lastPosition.Value;

            foreach (var platformMovable in _movables)
            {
                platformMovable.position += delta;
            }
            _lastPosition = _rigidbody.position;
        }

        void IDisposable.Dispose()
        {
            _triggerEventReceiver.OnTriggerEnter -= OnTriggerEnter;
            _triggerEventReceiver.OnTriggerExit -= OnTriggerExit;
        }
    }
}