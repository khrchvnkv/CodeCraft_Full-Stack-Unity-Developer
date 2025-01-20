namespace Game.Scripts.App.SaveLoad.Storage.Contracts
{
    public interface ILocalDataStorage
    {
        bool Write(in string data, out int version);
        bool Read(in int version, out string data);
        void Clear();
    }
}