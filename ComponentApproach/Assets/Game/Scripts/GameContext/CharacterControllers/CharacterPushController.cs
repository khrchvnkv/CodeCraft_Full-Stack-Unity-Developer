using Game.Scripts.GameObjects.Content.CharacterObject;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public class CharacterPushController : ITickable
    {
        private readonly CharacterProvider _characterProvider;
        
        private Character _character;
        
        public CharacterPushController(CharacterProvider characterProvider)
        {
            _characterProvider = characterProvider;
        }

        void ITickable.Tick() => ReadPushInput();

        private void ReadPushInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _character ??= _characterProvider.Value.Get<Character>();
                _character.RequestPush();
            }
        }
    }
}