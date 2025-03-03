using System;
using Game.Scripts.GameObjects.Core;
using Modules.Entity;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SpringboardObject
{
    public class Springboard : 
        IInitializable,
        IDisposable
    {
        private readonly TossComponent _tossComponent;
        private readonly TriggerEventReceiver _triggerEventReceiver;

        public Springboard(
            TossComponent tossComponent, 
            TriggerEventReceiver triggerEventReceiver)
        {
            _tossComponent = tossComponent;
            _triggerEventReceiver = triggerEventReceiver;
        }

        void IInitializable.Initialize() => _triggerEventReceiver.OnTriggerEnter += OnTriggerEnter;

        private void OnTriggerEnter(Entity entity) => _tossComponent.Toss(entity);

        void IDisposable.Dispose() => _triggerEventReceiver.OnTriggerEnter -= OnTriggerEnter;
    }
}