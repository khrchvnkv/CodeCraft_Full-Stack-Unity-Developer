using Game.Scripts.Components;
using Game.Scripts.Controllers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class WaypointMovementInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] _waypoints;

        public override void InstallBindings()
        {
            Container
                .Bind<WaypointMovementComponent>()
                .AsSingle()
                .WithArguments(_waypoints);
            
            Container
                .BindInterfacesAndSelfTo<WaypointMovementController>()
                .AsSingle();
        }
    }
}