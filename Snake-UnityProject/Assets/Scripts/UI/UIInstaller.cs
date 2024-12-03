using SnakeGame;
using Zenject;

namespace UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<ScreenManager>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<UIController>()
                .AsSingle();
            
            Container
                .Bind<IGameUI>()
                .To<GameUI>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}