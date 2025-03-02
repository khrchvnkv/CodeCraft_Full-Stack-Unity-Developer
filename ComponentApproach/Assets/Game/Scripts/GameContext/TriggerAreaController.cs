using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Content.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class TriggerAreaController : IInitializable, IDisposable
    {
        private readonly TriggerEventReceiver _receiver;
        private readonly HashSet<Rigidbody2D> _targets = new();
        
        public IReadOnlyCollection<Rigidbody2D> Targets => _targets;

        public TriggerAreaController(TriggerEventReceiver receiver)
        {
            _receiver = receiver;
        }
        
        void IInitializable.Initialize()
        {
            _receiver.OnTriggerEnter += OnTriggerEnter2D;
            _receiver.OnTriggerExit += OnTriggerExit2D;
        }

        void IDisposable.Dispose()
        {
            _receiver.OnTriggerEnter += OnTriggerEnter2D;
            _receiver.OnTriggerExit += OnTriggerExit2D;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Add(rb);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Remove(rb);
            }
        }
    }
}