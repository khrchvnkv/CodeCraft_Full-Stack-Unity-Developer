namespace Game.Scripts.GameObjects.Content.Entities
{
    public interface IEntity
    {
        T Get<T>();
        bool TryGet<T>(out T component) where T : class;
    }
}