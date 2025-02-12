using Game.Scripts.Components;
using Game.Scripts.Controllers;
using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _movementSpeed;

        public override void InstallBindings()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlatformController>()
                .AsSingle();

            Container
                .Bind<WaypointMovementComponent>()
                .AsSingle()
                .WithArguments(_waypoints);

            Container
                .Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(_movementSpeed);

            Container
                .BindInterfacesAndSelfTo<Platform>()
                .AsSingle();
        }
    }
}