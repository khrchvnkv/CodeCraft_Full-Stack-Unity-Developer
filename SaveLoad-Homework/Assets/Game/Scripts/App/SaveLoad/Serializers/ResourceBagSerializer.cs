using Game.Gameplay;

namespace Game.App
{
    public class ResourceBagSerializer : ComponentSerializer<ResourceBag, ResourceBagData>
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