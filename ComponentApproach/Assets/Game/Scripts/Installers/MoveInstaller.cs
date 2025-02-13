using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MoveInstaller : MonoInstaller
    {
        [SerializeField] private float _movementSpeed;

        public override void InstallBindings()
        {
            Container
                .Bind<MoveComponent>()
                .AsSingle()
                .WithArguments(_movementSpeed);
        }
    }
}