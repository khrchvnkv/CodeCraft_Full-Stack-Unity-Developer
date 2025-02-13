using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class SpringboardInstaller : MonoInstaller
    {
        [SerializeField] private Springboard _springboard;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Springboard>()
                .FromInstance(_springboard)
                .AsSingle();
        }
    }
}