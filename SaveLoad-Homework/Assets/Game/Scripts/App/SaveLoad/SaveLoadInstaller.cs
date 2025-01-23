using Zenject;

namespace Game.App
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EntityRepository>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<EntitySaveLoader>()
                .AsSingle();

            InstallSerializer<EntitiesSerializer>();
            
            InstallSerializer<CountdownSerializer>();
            InstallSerializer<DestinationSerializer>();
            InstallSerializer<HealthSerializer>();
            InstallSerializer<ProductionOrderSerializer>();
            InstallSerializer<ResourceBagSerializer>();
            InstallSerializer<TargetObjectSerializer>();
            InstallSerializer<TeamSerializer>();
        }

        private void InstallSerializer<T>() where T : EntitySerializer
        {
            Container
                .BindInterfacesAndSelfTo<T>()
                .AsCached();
        }
    }
}