using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public class CharacterMoveController : ITickable
    {
        private const string HorizontalAxis = "Horizontal";

        private readonly CharacterProvider _characterProvider;
        private MoveComponent _moveComponent;

        private Vector2 _movementInput;

        public CharacterMoveController(CharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;
        }

        void ITickable.Tick() => ReadMovementInput();
        
        private void ReadMovementInput()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            _movementInput = new Vector2(horizontal, 0);
            _moveComponent ??= _characterProvider.Value.Get<MoveComponent>();

            _moveComponent.SetDirection(_movementInput);
        }
    }
}