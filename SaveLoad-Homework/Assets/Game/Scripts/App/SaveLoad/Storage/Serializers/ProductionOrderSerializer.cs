using System.Collections.Generic;
using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.Gameplay.Components;
using Modules.Entities;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class ProductionOrderSerializer : MonoSerializer<ProductionOrder, ProductionOrderData>
    {
        private readonly EntityCatalog _entityCatalog;

        public ProductionOrderSerializer(EntityCatalog entityCatalog)
        {
            _entityCatalog = entityCatalog;
        }

        protected override ProductionOrderData SerializeComponent(in ProductionOrder component)
        {
            var entities = new string[component.Queue.Count];
            for (int i = 0; i < entities.Length; i++)
            {
                var entityInQueue = component.Queue[i].Name;
                entities[i] = entityInQueue;
            }

            return new ProductionOrderData
            {
                Queue = entities
            };
        }

        protected override void DeserializeComponent(in ProductionOrder component, in ProductionOrderData data)
        {
            var configs = new List<EntityConfig>(data.Queue.Length);
            for (var index = 0; index < data.Queue.Length; index++)
            {
                var entityName = data.Queue[index];
                if (_entityCatalog.FindConfig(entityName, out var config))
                {
                    configs.Add(config);
                }
            }

            component.Queue = configs;
        }
    }
}