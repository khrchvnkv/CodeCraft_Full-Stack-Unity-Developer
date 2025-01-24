using Game.Gameplay;
using Modules.Entities;

namespace Game.App
{
    public class TargetObjectSerializer : ComponentSerializer<TargetObject, TargetObjectDto>
    {
        private readonly EntityWorld _world;

        public override SerializationPriority Priority => SerializationPriority.Normal;

        public TargetObjectSerializer(EntityWorld world)
        {
            _world = world;
        }

        protected override TargetObjectDto SerializeComponent(in TargetObject component)
        {
            return new TargetObjectDto()
            {
                EntityId = component.Value != null ? component.Value.Id : -1
            };
        }

        protected override void DeserializeComponent(in TargetObject component, in TargetObjectDto data) => 
            component.Value = _world.TryGet(data.EntityId, out var target) ? target : null;
    }
}