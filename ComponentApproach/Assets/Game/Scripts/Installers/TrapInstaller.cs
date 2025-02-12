using Game.Scripts.Collision;
using Game.Scripts.Components;
using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Trap _trap;
        [SerializeField] private AttackCollision _attackCollision;
        [SerializeField] private int _damage;
        [SerializeField] private float _pushForce;

        public override void InstallBindings()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Trap>()
                .FromInstance(_trap)
                .AsSingle();

            Container
                .Bind<AttackCollision>()
                .FromInstance(_attackCollision)
                .AsSingle();

            Container
                .Bind<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);

            Container
                .Bind<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);
        }
    }
}