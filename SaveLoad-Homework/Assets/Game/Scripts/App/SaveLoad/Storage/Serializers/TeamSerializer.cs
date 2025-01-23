using Game.Gameplay;

namespace Game.App
{
    public class TeamSerializer : ComponentSerializer<Team, TeamData>
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