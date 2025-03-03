using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.PlatformObject
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private TriggerEventReceiver _platformMovablesTriggerEventReceiver;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Platform>()
                .AsSingle()
                .WithArguments(_platformMovablesTriggerEventReceiver);

            Container
                .BindInterfacesAndSelfTo<PlatformConditions>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_speed);

            Container
                .BindInterfacesAndSelfTo<WaypointMovementComponent>()
                .AsSingle()
                .WithArguments(_waypoints);

            Container
                .BindInterfacesAndSelfTo<WaypointMovementController>()
                .AsSingle();
        }
    }
}