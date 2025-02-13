using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class AttackInstaller : MonoInstaller
    {
        [SerializeField] private int _damage;
        
        public override void InstallBindings()
        {
            Container
                .Bind<AttackComponent>()
                .AsSingle()
                .WithArguments(_damage);
        }
    }
}