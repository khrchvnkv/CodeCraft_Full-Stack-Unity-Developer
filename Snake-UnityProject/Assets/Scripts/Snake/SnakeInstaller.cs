using Zenject;

namespace Snake
{
    public class SnakeInstaller : Installer<SnakeInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Modules.Snake>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container
                .BindInterfacesTo<SnakeMovementController>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<SnakeDifficultyController>()
                .AsSingle();

            Container
                .BindInterfacesTo<SnakeDeathController>()
                .AsSingle();
        }
    }
}