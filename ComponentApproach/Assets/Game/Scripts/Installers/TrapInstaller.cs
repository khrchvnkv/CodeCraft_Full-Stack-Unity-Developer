using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Trap _trap;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Trap>()
                .FromInstance(_trap)
                .AsSingle();
        }
    }
}