using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Content.Collision;
using Game.Scripts.GameObjects.Content.Entities;
using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private TrapEntity _entity;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _damage;
        [SerializeField] private float _force;
        [SerializeField] private CollisionEventReceiver _collisionEventReceiver;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<TrapEntity>()
                .FromInstance(_entity)
                .AsSingle();

            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Trap>()
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
                .WithArguments(_force);
        }
    }
}