using Zenject;

namespace Game.Scripts.UI
{
    //Don't modify
    public sealed class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<ControlsView>().FromComponentInHierarchy().AsSingle();
            this.Container.BindInterfacesTo<ControlsPresenter>().AsSingle();
        }
    }
}