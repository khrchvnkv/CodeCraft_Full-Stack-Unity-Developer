using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using Game.Scripts.GameObjects.View.Health;
using Game.Scripts.GameObjects.View.Jump;
using Game.Scripts.GameObjects.View.Push;
using Game.Scripts.GameObjects.View.ThrowUp;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.CharacterObject
{
    public class CharacterInstaller : MonoInstaller
    {
        [Header("Common")] 
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Ground Detecting")] 
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _layerMask;
        
        [Header("Health")] 
        [SerializeField] private int _hp;
        [SerializeField] private HealthViewComponent.HealthViewArgs _healthViewArgs;
        [SerializeField] private AudioSource _healthAudio;
        
        [Header("Jumping")] 
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _cooldown;
        [SerializeField] private AudioSource _jumpAudio;
        [SerializeField] private Transform _jumpAnimationTransform;
        [SerializeField] private JumpViewComponent.AnimationData _jumpAnimationData;

        [Header("Movement")] 
        [SerializeField] private float _speed;
        
        [Header("Push")] 
        [SerializeField] private TriggerEventReceiver _pushTrigger;
        [SerializeField] private float _pushForce;
        [SerializeField] private AudioSource _pushAudio;
        [SerializeField] private ParticleSystem _pushParticle;
        
        [Header("Toss")] 
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _tossForce;
        [SerializeField] private AudioSource _tossAudio;
        [SerializeField] private ParticleSystem _tossParticle;
        
        [Header("Rotation")] 
        [SerializeField] private Transform[] _rotatedTransforms;
        
        public override void InstallBindings()
        {
            InstallCommon();
            InstallComponents();
            InstallControllers();
        }

        private void InstallCommon()
        {
            Container
                .Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();
            
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
        }

        private void InstallComponents()
        {
            Container
                .BindInterfacesAndSelfTo<Character>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<CharacterConditions>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<GroundDetectorComponent>()
                .AsSingle()
                .WithArguments(_collider, _layerMask);
            
            Container
                .BindInterfacesAndSelfTo<HealthComponent>()
                .AsSingle()
                .WithArguments(_hp);
            
            Container
                .BindInterfacesAndSelfTo<HealthViewComponent>()
                .AsSingle()
                .WithArguments(_healthViewArgs);

            Container
                .BindInterfacesAndSelfTo<HealthAudioComponent>()
                .AsSingle()
                .WithArguments(_healthAudio);
            
            Container
                .BindInterfacesAndSelfTo<JumpComponent>()
                .AsSingle()
                .WithArguments(_jumpForce, _cooldown);

            Container
                .BindInterfacesAndSelfTo<JumpAudioComponent>()
                .AsSingle()
                .WithArguments(_jumpAudio);
            
            Container
                .BindInterfacesAndSelfTo<JumpViewComponent>()
                .AsSingle()
                .WithArguments(_jumpAnimationTransform, _jumpAnimationData);
            
            Container
                .BindInterfacesAndSelfTo<MoveComponent>()
                .AsSingle()
                .WithArguments(_speed);
            
            Container
                .BindInterfacesAndSelfTo<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);

            Container
                .BindInterfacesAndSelfTo<PushAudioComponent>()
                .AsSingle()
                .WithArguments(_pushAudio);

            Container
                .BindInterfacesAndSelfTo<PushViewComponent>()
                .AsSingle()
                .WithArguments(_pushParticle);
            
            Container
                .BindInterfacesAndSelfTo<TriggerDetector>()
                .AsSingle()
                .WithArguments(_pushTrigger);
            
            Container
                .BindInterfacesAndSelfTo<TossComponent>()
                .AsSingle()
                .WithArguments(_direction, _tossForce);

            Container
                .BindInterfacesAndSelfTo<TossAudioComponent>()
                .AsSingle()
                .WithArguments(_tossAudio);

            Container
                .BindInterfacesAndSelfTo<TossViewComponent>()
                .AsSingle()
                .WithArguments(_tossParticle);
            
            Container
                .BindInterfacesAndSelfTo<RotateComponent>()
                .AsSingle()
                .WithArguments(_rotatedTransforms);
        }

        private void InstallControllers()
        {
            Container
                .BindInterfacesAndSelfTo<RotateByMovementController>()
                .AsSingle();
        }
    }
}