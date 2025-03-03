using Modules.Entity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public class CharacterControlInstaller : MonoInstaller
    {
        [SerializeField] private Entity _character;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CharacterProvider>()
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