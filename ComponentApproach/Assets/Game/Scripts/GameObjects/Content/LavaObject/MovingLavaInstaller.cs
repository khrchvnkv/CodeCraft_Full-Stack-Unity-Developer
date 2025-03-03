using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.LavaObject
{
    public class MovingLavaInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _speed;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<MovingLavaConditions>()
                .AsSingle();

            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
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