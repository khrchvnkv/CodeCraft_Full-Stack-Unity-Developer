using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlanetPopup>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container
                .Bind<MoneyView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}