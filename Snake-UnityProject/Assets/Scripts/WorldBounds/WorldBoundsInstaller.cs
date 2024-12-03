using SnakeGame;
using Zenject;

namespace WorldBounds
{
    public class WorldBoundsInstaller : Installer<WorldBoundsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IWorldBounds>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}