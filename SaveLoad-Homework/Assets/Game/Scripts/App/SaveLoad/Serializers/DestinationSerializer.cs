using Game.Gameplay;

namespace Game.App
{
    public class DestinationSerializer : ComponentSerializer<DestinationPoint, DestinationPointData>
    {
        public override SerializationPriority Priority => SerializationPriority.Normal;

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