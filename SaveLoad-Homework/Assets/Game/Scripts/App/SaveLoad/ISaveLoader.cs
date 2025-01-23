using Cysharp.Threading.Tasks;

namespace Game.App
{
    public interface ISaveLoader
    {
        UniTask<(bool, int)> Save();
        UniTask<bool> Load(int version);
    }
}