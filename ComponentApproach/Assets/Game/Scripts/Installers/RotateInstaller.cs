using Game.Scripts.Components;
using Game.Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class RotateInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _transforms;

        public override void InstallBindings()
        {
            Container
                .Bind<RotateComponent>()
                .AsSingle()
                .WithArguments(_transforms);
            
            Container
                .BindInterfacesAndSelfTo<RotateByMovementController>()
                .AsSingle();
        }
    }
}