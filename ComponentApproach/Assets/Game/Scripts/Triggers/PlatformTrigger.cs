using System.Collections.Generic;
using Game.Scripts.Contracts;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class PlatformTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private readonly HashSet<IPlatformMovable> _platformMovables = new();

        private Vector2? _lastPosition;
        
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

        private void FixedUpdate()
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
    }
}