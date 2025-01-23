using Cysharp.Threading.Tasks;

namespace Game.App

{
    public interface IRepository
    {
        UniTask<(bool, string)> GetData(int version);
        UniTask<(bool, int)> SetData(string data);
    }
}