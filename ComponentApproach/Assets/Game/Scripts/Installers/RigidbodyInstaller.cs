using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class RigidbodyInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;

        public override void InstallBindings()
        {
            Container
                .Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
        }
    }
}