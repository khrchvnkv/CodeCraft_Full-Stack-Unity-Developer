using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class CountdownSerializer : MonoSerializer<Countdown, CountdownData>
    {
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