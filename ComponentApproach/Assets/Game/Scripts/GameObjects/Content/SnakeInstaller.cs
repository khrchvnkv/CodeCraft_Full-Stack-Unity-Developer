using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Content.Collision;
using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using Game.Scripts.GameObjects.View.Health;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private SnakeEntity _entity;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Movement")] 
        [SerializeField] private float _speed;
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private Transform[] _rotateTransforms;

        [Header("Health")] 
        [SerializeField] private int _health;
        [SerializeField] private AudioSource _healthAudio;
        [SerializeField] private HealthViewComponent.HealthViewArgs _healthViewArgs;
        
        [Header("Attack")] 
        [SerializeField] private int _damage;
        [SerializeField] private CollisionEventReceiver _attackCollision;

        [Header("Toss")] 
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _tossForce;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Snake>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<SnakeEntity>()
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
                .BindInterfacesAndSelfTo<HealthAudioComponent>()
                .AsSingle()
                .WithArguments(_healthAudio);
            
            Container
                .BindInterfacesAndSelfTo<HealthViewComponent>()
                .AsSingle()
                .WithArguments(_healthViewArgs);
            
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
                .WithArguments(_attackCollision);
            
            Container
                .BindInterfacesAndSelfTo<TossComponent>()
                .AsSingle()
                .WithArguments(_direction, _tossForce);

            Container
                .BindInterfacesAndSelfTo<RotateComponent>()
                .AsSingle()
                .WithArguments(_rotateTransforms);

            Container
                .BindInterfacesAndSelfTo<RotateByMovementController>()
                .AsSingle();
        }
    }
}