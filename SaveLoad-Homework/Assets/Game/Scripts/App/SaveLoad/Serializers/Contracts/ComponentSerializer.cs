using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.App
{
    public abstract class ComponentSerializer<TComponent, TData> : EntitySerializer where TComponent : Component
    {
        protected override void SerializeData(in IDictionary<string, string> dataContainer, in IReadOnlyCollection<Entity> entities)
        {
            var data = new Dictionary<int, TData>();
            
            foreach (var entity in entities)
            {
                if (entity.TryGetComponent(out TComponent component))
                {
                    var serializedComponent = SerializeComponent(component);
                    data[entity.Id] = serializedComponent;
                }
            }

            dataContainer[SerializerKey] = JsonConvert.SerializeObject(data);
        }

        protected override void DeserializeData(in IDictionary<string, string> dataContainer, in EntityWorld world)
        {
            if (dataContainer.TryGetValue(SerializerKey, out var value))
            {
                var dataMap = JsonConvert.DeserializeObject<Dictionary<int, TData>>(value);
                foreach (var (id, data) in dataMap)
                {
                    var entity = world.Get(id);
                    if (entity.TryGetComponent(out TComponent component))
                    {
                        DeserializeComponent(component, data);
                    }
                }
            }
        }

        protected abstract TData SerializeComponent(in TComponent component);
        
        protected abstract void DeserializeComponent(in TComponent component, in TData data);
    }
}