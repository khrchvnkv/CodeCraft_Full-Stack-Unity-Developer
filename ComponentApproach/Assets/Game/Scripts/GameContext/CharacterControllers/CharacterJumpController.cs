using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public class CharacterJumpController : ITickable
    {
        private readonly CharacterProvider _characterProvider;
        
        private JumpComponent _jumpComponent;
        
        public CharacterJumpController(CharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;
        }

        void ITickable.Tick() => ReadJumpInput();
        
        private void ReadJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpComponent ??= _characterProvider.Value.Get<JumpComponent>();
                _jumpComponent.RequestJump();
            }
        }
    }
}