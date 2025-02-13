using Game.Scripts.Objects;
using Game.Scripts.Triggers;
using UnityEngine;
using Zenject;
using CharacterController = Game.Scripts.Controllers.CharacterController;

namespace Game.Scripts.Installers
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private PushTrigger _pushTrigger;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Character>()
                .FromComponentsInHierarchy()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterController>()
                .AsSingle()
                .WithArguments(_pushTrigger);
        }
    }
}