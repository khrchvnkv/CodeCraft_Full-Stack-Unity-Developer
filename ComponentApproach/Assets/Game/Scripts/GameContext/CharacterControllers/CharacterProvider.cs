using Modules.Entity;

namespace Game.Scripts.GameContext.CharacterControllers
{
    public sealed class CharacterProvider
    {
        public IEntity Value { get; }

        public CharacterProvider(IEntity value)
        {
            Value = value;
        }
    }
}