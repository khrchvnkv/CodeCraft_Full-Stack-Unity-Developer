using Game.Gameplay;

namespace Game.App
{
    public class HealthSerializer : ComponentSerializer<Health, HealthData>
    {
        public override SerializationPriority Priority => SerializationPriority.Normal;

        protected override HealthData SerializeComponent(in Health component)
        {
            return new HealthData
            {
                Current = component.Current
            };
        }

        protected override void DeserializeComponent(in Health component, in HealthData data) => 
            component.Current = data.Current;
    }
}