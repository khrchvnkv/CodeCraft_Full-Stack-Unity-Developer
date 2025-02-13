using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PushInstaller : MonoInstaller
    {
        [SerializeField] private float _pushForce;

        public override void InstallBindings()
        {
            Container
                .Bind<PushComponent>()
                .AsSingle()
                .WithArguments(_pushForce);
        }
    }
}