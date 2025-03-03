using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.TrapObject
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _damage;
        [SerializeField] private float _force;
        [SerializeField] private CollisionEventReceiver _collisionEventReceiver;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Trap>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<DestroyableComponent>()
                .AsSingle()
                .WithArguments(gameObject);
            
            Container
                .BindInterfacesAndSelfTo<TrapConditions>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);

            Container
                .BindInterfacesAndSelfTo<CollisionEventReceiver>()
                .FromInstance(_collisionEventReceiver)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PushComponent>()
                .AsSingle()
                .WithArguments(_force);
        }
    }
}