using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.Character
{
    public class CharacterTossController : ITickable, IFixedTickable
    {
        private readonly EntityProvider _entityProvider;
        
        private TossComponent _tossComponent;
        private TriggerAreaController _triggerAreaController;
        private bool _isThrowUpRequest;

        public CharacterTossController(EntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }

        void ITickable.Tick() => ReadMovementInput();

        void IFixedTickable.FixedTick() => ThrowUp();

        private void ReadMovementInput()
        {
            if (Input.GetMouseButtonDown(1) && !_isThrowUpRequest)
            {
                _isThrowUpRequest = true;
            }
        }

        private void ThrowUp()
        {
            if (!_isThrowUpRequest)
            {
                return;
            }

            if (_isThrowUpRequest)
            {
                _tossComponent ??= _entityProvider.Value.Get<TossComponent>();
                _triggerAreaController ??= _entityProvider.Value.Get<TriggerAreaController>();

                var targets = _triggerAreaController.Targets;
                _tossComponent.Toss(targets);
            }
            _isThrowUpRequest = false;
        }
    }
}