using System.Linq;
using System.Reflection;
using Game.Scripts.App.SaveLoad.Storage;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts;
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
                    .BindInterfacesAndSelfTo<LocalFileDataStorage>()
                    .AsSingle()
                    .WithArguments(Application.streamingAssetsPath, "save_{0}.txt");
            }
            else
            {
                Container
                    .BindInterfacesAndSelfTo<PlayerPrefsDataStorage>()
                    .AsSingle();
            }
            
            Container
                .BindInterfacesAndSelfTo<RemoteDataDataStorage>()
                .AsSingle()
                .WithArguments("http://127.0.0.1:8888");

            InstallSerializers();
        }
        
        private void InstallSerializers()
        {
            var monoSerializerType = typeof(BaseSerializer);
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