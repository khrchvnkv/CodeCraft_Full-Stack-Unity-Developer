using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class TransformInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;

        public override void InstallBindings()
        {
            Container
                .Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();
        }
    }
}