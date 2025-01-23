using UnityEngine;
using Zenject;

namespace Game.App
{
    public class StorageInstaller : MonoInstaller
    {
        [SerializeField] private bool _useLocalFileStorage;

        public override void InstallBindings()
        {
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
        }
    }
}