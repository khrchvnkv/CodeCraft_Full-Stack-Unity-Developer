namespace Game.Scripts.App.SaveLoad
{
    public interface ISaveLoader
    {
        bool Save(out int version);
        bool Load(in int version);
    }
}