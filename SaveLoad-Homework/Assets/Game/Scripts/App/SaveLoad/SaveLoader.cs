using System.Collections.Generic;
using Game.Scripts.App.SaveLoad.Storage;
using Game.Scripts.App.SaveLoad.Storage.Serializers;
using Modules.Entities;
using Newtonsoft.Json;

namespace Game.Scripts.App.SaveLoad
{
    public class SaveLoader : ISaveLoader
    {
        private readonly ISerializer[] _serializers;
        private readonly IDataStorage _storage;
        private readonly EntityWorld _world;

        public SaveLoader(
            ISerializer[] serializers, 
            IDataStorage storage,
            EntityWorld world)
        {
            _serializers = serializers;
            _storage = storage;
            _world = world;
        }

        public bool Save(out int version)
        {
            var dataContainer = new Dictionary<string, string>();
            foreach (var serializer in _serializers)
            {
                serializer.Serialize(dataContainer, _world);
            }

            var data = JsonConvert.SerializeObject(dataContainer);
            return _storage.Write(data, out version);
        }

        public bool Load(in int version)
        {
            if (_storage.Read(version, out var data))
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