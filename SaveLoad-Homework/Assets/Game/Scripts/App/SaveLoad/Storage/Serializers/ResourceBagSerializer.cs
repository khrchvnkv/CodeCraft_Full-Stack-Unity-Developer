using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Enums;
using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class ResourceBagSerializer : MonoSerializer<ResourceBag, ResourceBagData>
    {
        public override SerializationPriority Priority => SerializationPriority.Normal;

        protected override ResourceBagData SerializeComponent(in ResourceBag component)
        {
            return new ResourceBagData()
            {
                Type = component.Type,
                Current = component.Current
            };
        }

        protected override void DeserializeComponent(in ResourceBag component, in ResourceBagData data)
        {
            component.Type = data.Type;
            component.Current = data.Current;
        }
    }
}