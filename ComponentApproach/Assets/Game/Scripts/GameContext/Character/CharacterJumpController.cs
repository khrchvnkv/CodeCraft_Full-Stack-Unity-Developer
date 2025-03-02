using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.Character
{
    public class CharacterJumpController : ITickable, IFixedTickable
    {
        private readonly EntityProvider _entityProvider;
        
        private JumpComponent _jumpComponent;
        private bool _isJumpRequest;

        public CharacterJumpController(EntityProvider entityProvider)
        {
            _entityProvider = entityProvider;
        }

        void ITickable.Tick() => ReadJumpInput();

        void IFixedTickable.FixedTick() => HandleJumpInput();

        private void ReadJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isJumpRequest)
            {
                _isJumpRequest = true;
            }
        }

        private void HandleJumpInput()
        {
            if (_isJumpRequest)
            {
                _jumpComponent ??= _entityProvider.Value.Get<JumpComponent>();
                _jumpComponent.Jump();
                _isJumpRequest = false;  
            }
        }
    }
}