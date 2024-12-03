using Zenject;

namespace GameCycle
{
    public class GameCycleInstaller : Installer<GameCycleInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<GameCycle>()
                .AsSingle();
        }
    }
}