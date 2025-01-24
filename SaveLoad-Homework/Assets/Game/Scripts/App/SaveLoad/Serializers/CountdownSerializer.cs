using Game.Gameplay;

namespace Game.App
{
    public class CountdownSerializer : ComponentSerializer<Countdown, CountdownData>
    {
        public override SerializationPriority Priority => SerializationPriority.Normal;

        protected override CountdownData SerializeComponent(in Countdown component)
        {
            return new CountdownData
            {
                Current = component.Current
            };
        }

        protected override void DeserializeComponent(in Countdown component, in CountdownData data) => 
            component.Current = data.Current;
    }
}