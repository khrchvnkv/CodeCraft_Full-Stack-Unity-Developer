using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.SaveLoad.Storage.Contracts;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts;
using Modules.Entities;
using Newtonsoft.Json;

namespace Game.Scripts.App.SaveLoad
{
    public class SaveLoader : ISaveLoader
    {
        private readonly ISerializer[] _serializers;
        private readonly ILocalDataStorage _localDataStorage;
        private readonly IRemoteDataStorage _remoteDataStorage;
        private readonly EntityWorld _world;

        public SaveLoader(
            ISerializer[] serializers, 
            ILocalDataStorage localDataStorage,
            IRemoteDataStorage remoteDataStorage,
            EntityWorld world)
        {
            _serializers = serializers.OrderBy(x => x.Priority).ToArray();
            _localDataStorage = localDataStorage;
            _remoteDataStorage = remoteDataStorage;
            _world = world;
        }

        public async UniTask<(bool, int)> Save()
        {
            var dataContainer = new Dictionary<string, string>();
            foreach (var serializer in _serializers)
            {
                serializer.Serialize(dataContainer, _world);
            }

            var data = JsonConvert.SerializeObject(dataContainer);
            var localResult = _localDataStorage.Write(data, out var version);
            if (localResult)
            {
                var remoteResult = await _remoteDataStorage.Write(version, data);
                return (remoteResult, version);
            }

            return (false, -1);
        }

        public async UniTask<bool> Load(int version)
        {
            var result = _localDataStorage.Read(version, out var data);
            if (!result)
            {
                (result, data) = await _remoteDataStorage.Read(version);
            }
            
            if (result)
            {
                _world.DestroyAll();
                var dataContainer = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                foreach (var serializer in _serializers)
                {
                    serializer.Deserialize(dataContainer, _world);
                }

                return true;
            }

            return false;
        }
    }
}