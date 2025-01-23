using System.Collections.Generic;
using Modules.Entities;

namespace Game.App
{
    public interface ISerializer
    {
        SerializationPriority Priority { get; }
        
        void Serialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
        
        void Deserialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
    }
}