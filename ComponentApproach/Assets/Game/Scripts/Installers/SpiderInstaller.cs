using Game.Scripts.Collision;
using Game.Scripts.Components;
using Game.Scripts.Controllers;
using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class SpiderInstaller : MonoInstaller
    {
        [SerializeField] private Spider _spider;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private AttackCollision _attackCollision;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _pushForce;
        [SerializeField] private int _hp;
        [SerializeField] private int _damage;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Spider>()
                .FromInstance(_spider)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<SpiderController>()
                .AsSingle();
            
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(_movementSpeed);

            Container
                .Bind<WaypointMovementComponent>()
                .AsSingle()
                .WithArguments(_waypoints);

            Container
                .Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(gameObject, _hp);

            Container
                .Bind<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);

            Container
                .Bind<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);
            
            Container
                .Bind<AttackCollision>()
                .FromInstance(_attackCollision)
                .AsSingle();
        }
    }
}