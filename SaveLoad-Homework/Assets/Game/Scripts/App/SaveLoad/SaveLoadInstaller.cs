using System.Linq;
using System.Reflection;
using Game.Scripts.App.SaveLoad.Storage;
using Game.Scripts.App.SaveLoad.Storage.Serializers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.SaveLoad
{
    public class SaveLoadInstaller : MonoInstaller
    {
        [SerializeField] private bool _useLocalFileStorage;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SaveLoader>()
                .AsSingle();

            if (_useLocalFileStorage)
            {
                Container
                    .BindInterfacesAndSelfTo<LocalFileStorage>()
                    .AsSingle()
                    .WithArguments(Application.streamingAssetsPath, "save_{0}.txt");
            }
            else
            {
                Container
                    .BindInterfacesAndSelfTo<PlayerPrefsStorage>()
                    .AsSingle();
            }

            Container
                .BindInterfacesAndSelfTo<EntitiesSerializer>()
                .AsCached();
            
            InstallMonoSerializers();
        }
        
        private void InstallMonoSerializers()
        {
            var monoSerializerType = typeof(BaseSerializer);
            var types = Assembly
                .GetAssembly(monoSerializerType)
                .GetTypes()
                .Where(t => monoSerializerType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
                .ToHashSet();
            
            types.Remove(typeof(EntitiesSerializer));
            
            foreach (var type in types)
            {
                Container
                    .BindInterfacesAndSelfTo(type)
                    .AsCached();
            }
        }
    }
}