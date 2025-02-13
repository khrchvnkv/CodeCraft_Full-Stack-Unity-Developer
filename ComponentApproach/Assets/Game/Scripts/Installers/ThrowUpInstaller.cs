using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ThrowUpInstaller : MonoInstaller
    {
        [SerializeField] private float _throwUpForce;
        [SerializeField] private Vector2 _direction = Vector2.up;

        public override void InstallBindings()
        {
            Container
                .Bind<ThrowUpComponent>()
                .AsSingle()
                .WithArguments(_throwUpForce, _direction);
        }
    }
}