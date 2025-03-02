using Game.Scripts.GameContext.Character;
using Game.Scripts.GameObjects.Content.Entities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content
{
    public class CharacterControlInstaller : MonoInstaller
    {
        [SerializeField] private CharacterEntity _character;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EntityProvider>()
                .AsSingle()
                .WithArguments(_character);

            Container
                .BindInterfacesAndSelfTo<CharacterJumpController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterMoveController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterPushController>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterTossController>()
                .AsSingle();
        }
    }
}