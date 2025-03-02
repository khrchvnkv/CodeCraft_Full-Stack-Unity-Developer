namespace Game.Scripts.GameObjects.Content.Entities
{
    public sealed class EntityProvider
    {
        public IEntity Value { get; }

        public EntityProvider(IEntity value)
        {
            Value = value;
        }
    }
}