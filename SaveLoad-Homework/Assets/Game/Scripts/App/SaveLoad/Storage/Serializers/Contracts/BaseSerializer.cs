using System.Collections.Generic;
using System.Linq;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Enums;
using Modules.Entities;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts
{
    public abstract class BaseSerializer : ISerializer
    {
        protected string SerializerKey => GetType().FullName;

        public abstract SerializationPriority Priority { get; }

        void ISerializer.Serialize(in IDictionary<string, string> dataContainer, in EntityWorld world)
        {
            ClearDataInContainer(dataContainer);
            SerializeData(dataContainer, GetEntities(world));
        }

        void ISerializer.Deserialize(in IDictionary<string, string> dataContainer, in EntityWorld world) => 
            DeserializeData(dataContainer, world);


        protected abstract void SerializeData(in IDictionary<string, string> dataContainer, in Entity[] entities);

        protected abstract void DeserializeData(in IDictionary<string, string> dataContainer, in EntityWorld world);
        
        private void ClearDataInContainer(in IDictionary<string, string> dataContainer) => dataContainer.Remove(SerializerKey);

        private Entity[] GetEntities(in EntityWorld world) => world.GetAll().ToArray();
    }
}