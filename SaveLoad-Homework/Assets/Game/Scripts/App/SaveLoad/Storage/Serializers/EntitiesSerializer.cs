using System.Collections.Generic;
using Game.Scripts.App.SaveLoad.Storage.Dto;
using Modules.Entities;
using Newtonsoft.Json;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class EntitiesSerializer : BaseSerializer
    {
        protected override void SerializeData(in IDictionary<string, string> dataContainer, in Entity[] entities)
        {
            var data = new EntityData[entities.Length];

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var transform = entity.transform;
                var entityData = new EntityData
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Position = transform.position,
                    Rotation = transform.rotation
                };

                data[i] = entityData;
            }

            dataContainer[SerializerKey] = JsonConvert.SerializeObject(data);
        }

        protected override void DeserializeData(in IDictionary<string, string> dataContainer, in EntityWorld world)
        {
            if (dataContainer.TryGetValue(SerializerKey, out var value))
            {
                var datas = JsonConvert.DeserializeObject<EntityData[]>(value);
                foreach (var data in datas)
                {
                    world.Spawn(data.Name, data.Position, data.Rotation, data.Id);
                }
            }
        }
    }
}