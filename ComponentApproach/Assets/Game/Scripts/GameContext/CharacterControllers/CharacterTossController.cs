using Game.Scripts.GameObjects.Content.CharacterObject;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public class CharacterTossController : ITickable
    {
        private readonly CharacterProvider _characterProvider;
        
        private Character _character;

        public CharacterTossController(CharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;
        }

        void ITickable.Tick() => ReadMovementInput();

        private void ReadMovementInput()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _character = _characterProvider.Value.Get<Character>();
                _character.RequestToss();
            }
        }
    }
}