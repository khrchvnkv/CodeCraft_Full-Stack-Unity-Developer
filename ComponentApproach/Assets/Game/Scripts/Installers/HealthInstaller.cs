using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class HealthInstaller : MonoInstaller
    {
        [SerializeField] private int _hp;

        public override void InstallBindings()
        {
            Container
                .Bind<HealthComponent>()
                .AsSingle()
                .WithArguments(_hp);
        }
    }
}