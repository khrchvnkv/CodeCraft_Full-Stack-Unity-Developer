using System.Linq;
using System.Reflection;
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

            InstallSerializers();
        }
        
        private void InstallSerializers()
        {
            var monoSerializerType = typeof(EntitySerializer);
            var types = Assembly
                .GetAssembly(monoSerializerType)
                .GetTypes()
                .Where(t => monoSerializerType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            
            foreach (var type in types)
            {
                Container
                    .BindInterfacesAndSelfTo(type)
                    .AsCached();
            }
        }
    }
}