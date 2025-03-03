using System;
using System.Collections.Generic;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class TriggerDetector : IInitializable, IDisposable
    {
        private readonly TriggerEventReceiver _receiver;
        private readonly HashSet<Rigidbody2D> _targets = new();
        
        public IReadOnlyCollection<Rigidbody2D> Targets => _targets;

        public TriggerDetector(TriggerEventReceiver receiver)
        {
            _receiver = receiver;
        }
        
        void IInitializable.Initialize()
        {
            _receiver.OnTriggerEnter += OnTriggerEnter;
            _receiver.OnTriggerExit += OnTriggerExit;
        }

        void IDisposable.Dispose()
        {
            _receiver.OnTriggerEnter += OnTriggerEnter;
            _receiver.OnTriggerExit += OnTriggerExit;
        }
        
        private void OnTriggerEnter(Entity entity)
        {
            if (entity.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Add(rb);
            }
        }

        private void OnTriggerExit(Entity entity)
        {
            if (entity.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Remove(rb);
            }
        }
    }
}