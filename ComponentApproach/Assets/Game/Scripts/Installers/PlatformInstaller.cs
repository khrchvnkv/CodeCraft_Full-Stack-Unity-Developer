using Game.Scripts.Objects;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlatformInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Platform>()
                .AsSingle();
        }
    }
}