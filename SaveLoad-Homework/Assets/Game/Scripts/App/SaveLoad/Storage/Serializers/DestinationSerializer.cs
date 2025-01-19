using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class DestinationSerializer : MonoSerializer<DestinationPoint, DestinationPointData>
    {
        protected override DestinationPointData SerializeComponent(in DestinationPoint component)
        {
            return new DestinationPointData()
            {
                Value = component.Value
            };
        }

        protected override void DeserializeComponent(in DestinationPoint component, in DestinationPointData data) => 
            component.Value = data.Value;
    }
}