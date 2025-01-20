using Game.Scripts.App.SaveLoad.Storage.Dto;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Contracts;
using Game.Scripts.App.SaveLoad.Storage.Serializers.Enums;
using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.App.SaveLoad.Storage.Serializers
{
    public class TeamSerializer : MonoSerializer<Team, TeamData>
    {
        public override SerializationPriority Priority => SerializationPriority.Normal;

        protected override TeamData SerializeComponent(in Team component)
        {
            return new TeamData()
            {
                Type = component.Type
            };
        }

        protected override void DeserializeComponent(in Team component, in TeamData data)
        {
            component.Type = data.Type;
        }
    }
}