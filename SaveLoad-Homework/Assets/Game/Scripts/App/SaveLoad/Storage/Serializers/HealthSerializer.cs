using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class HealthSerializer : MonoSerializer<Health, HealthData>
    {
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