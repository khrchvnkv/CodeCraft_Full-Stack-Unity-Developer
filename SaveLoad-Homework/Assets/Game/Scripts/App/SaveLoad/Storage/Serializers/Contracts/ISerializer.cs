using System.Collections.Generic;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Enums;
using Modules.Entities;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts
{
    public interface ISerializer
    {
        SerializationPriority Priority { get; }
        
        void Serialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
        
        void Deserialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
    }
}