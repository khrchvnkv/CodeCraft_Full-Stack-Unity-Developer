using Cysharp.Threading.Tasks;

namespace Game.Scripts.App.SaveLoad.Storage.Contracts
{
    public interface IRemoteDataStorage
    {
        UniTask<bool> Write(int version, string data);

        UniTask<(bool, string)> Read(int version);
    }
}