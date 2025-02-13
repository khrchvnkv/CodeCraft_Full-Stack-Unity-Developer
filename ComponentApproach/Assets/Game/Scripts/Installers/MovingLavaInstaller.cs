using Game.Scripts.Objects;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MovingLavaInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<MovingLava>()
                .AsSingle();
        }
    }
}