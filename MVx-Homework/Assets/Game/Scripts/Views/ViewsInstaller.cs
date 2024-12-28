using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<PlanetPopup>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MoneyView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}