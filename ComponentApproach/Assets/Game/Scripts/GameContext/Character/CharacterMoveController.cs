using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.Character
{
    public class CharacterMoveController : ITickable, IFixedTickable
    {
        private const string HorizontalAxis = "Horizontal";
        
        private readonly EntityProvider _entityProvider;

        private Vector2 _movementInput;
        private MoveComponent _moveComponent;

        public CharacterMoveController(EntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }

        void ITickable.Tick() => ReadMovementInput();

        void IFixedTickable.FixedTick() => HandleMovementInput();

        private void ReadMovementInput()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            _movementInput = new Vector2(horizontal, 0);
        }

        private void HandleMovementInput()
        {
            _moveComponent ??= _entityProvider.Value.Get<MoveComponent>();
            _moveComponent.Move(_movementInput);
        }
    }
}