using System.Collections.Generic;
using Modules.Entities;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public interface ISerializer
    {
        void Serialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
        
        void Deserialize(in IDictionary<string, string> dataContainer, in EntityWorld world);
    }
}