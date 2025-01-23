using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Entities;
using Newtonsoft.Json;

namespace Game.App
{
    public class EntitySaveLoader : ISaveLoader
    {
        private readonly ISerializer[] _serializers;
        private readonly IRepository _repository;
        private readonly EntityWorld _world;

        public EntitySaveLoader(
            ISerializer[] serializers, 
            IRepository repository,
            EntityWorld world)
        {
            _serializers = serializers.OrderBy(x => x.Priority).ToArray();
            _repository = repository;
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
            var saveResult = await _repository.SetData(data);
            return saveResult;
        }

        public async UniTask<bool> Load(int version)
        {
            var (result, data) = await _repository.GetData(version);
            
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