using Cysharp.Threading.Tasks;

namespace Game.App
{
    public interface IRemoteDataStorage
    {
        UniTask<bool> Write(int version, string data);

        UniTask<(bool, string)> Read(int version);
    }
}