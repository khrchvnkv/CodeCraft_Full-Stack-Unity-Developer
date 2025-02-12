using Game.Scripts.Components;
using Game.Scripts.Objects;
using Game.Scripts.Triggers;
using UnityEngine;
using Zenject;
using CharacterController = Game.Scripts.Controllers.CharacterController;

namespace Game.Scripts.Installers
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;
        [SerializeField] private float _pushForce;
        [SerializeField] private float _throwUpForce;
        [SerializeField] private int _startHp;
        [SerializeField] private PushTrigger _pushTrigger;

        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();
            
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<Character>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container
                .Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(gameObject, _startHp);
            
            Container
                .Bind<JumpComponent>()
                .AsSingle()
                .WithArguments(_jumpForce, _jumpCooldown);
            
            Container
                .Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(_movementSpeed);
            
            Container
                .Bind<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);
            
            Container
                .Bind<ThrowUpComponent>()
                .AsSingle()
                .WithArguments(_throwUpForce);
            
            Container
                .Bind<RotateComponent>()
                .AsSingle();

            Container
                .Bind<HealthViewComponent>()
                .FromComponentInChildren()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterController>()
                .AsSingle()
                .WithArguments(_pushTrigger);
        }
    }
}