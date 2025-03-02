using System;
using Game.Scripts.GameObjects.Content.Triggers;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext
{
    public class TossInAreaController : IInitializable, IDisposable
    {
        private readonly TossComponent _tossComponent;
        private readonly TriggerEventReceiver _triggerEventReceiver;

        public TossInAreaController(
            TossComponent tossComponent,
            TriggerEventReceiver triggerEventReceiver)
        {
            _tossComponent = tossComponent;
            _triggerEventReceiver = triggerEventReceiver;
        }
        
        void IInitializable.Initialize() => _triggerEventReceiver.OnTriggerEnter += OnTriggerEnter2D;

        void IDisposable.Dispose() => _triggerEventReceiver.OnTriggerEnter -= OnTriggerEnter2D;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                _tossComponent.Toss(rb);
            }
        }
    }
}