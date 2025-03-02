using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Content.Collision;
using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using Game.Scripts.GameObjects.View.Health;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content
{
    public class SpiderInstaller : MonoInstaller
    {
        [SerializeField] private SpiderEntity _entity;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Health")] 
        [SerializeField] private int _health;
        [SerializeField] private AudioSource _healthAudio;
        [SerializeField] private HealthViewComponent.HealthViewArgs _healthViewArgs;
        
        [Header("Movement")]
        [SerializeField] private float _speed;
        [SerializeField] private Transform[] _waypoints;

        [Header("Attack")] 
        [SerializeField] private int _damage;
        [SerializeField] private CollisionEventReceiver _collisionEventReceiver;
        
        [Header("Push")]
        [SerializeField] private float _pushForce;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Spider>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SpiderEntity>()
                .FromInstance(_entity)
                .AsSingle();
            
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<HealthComponent>()
                .AsSingle()
                .WithArguments(_health);

            Container
                .BindInterfacesAndSelfTo<HealthViewComponent>()
                .AsSingle()
                .WithArguments(_healthViewArgs);

            Container
                .BindInterfacesAndSelfTo<HealthAudioComponent>()
                .AsSingle()
                .WithArguments(_healthAudio);

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

            Container
                .BindInterfacesAndSelfTo<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);
            
            Container
                .BindInterfacesAndSelfTo<AttackController>()
                .AsSingle()
                .WithArguments(_collisionEventReceiver);

            Container
                .BindInterfacesAndSelfTo<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);
        }
    }
}