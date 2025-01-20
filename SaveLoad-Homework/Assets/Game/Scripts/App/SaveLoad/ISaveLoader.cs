using Cysharp.Threading.Tasks;

namespace Game.Scripts.App.SaveLoad
{
    public interface ISaveLoader
    {
        UniTask<(bool, int)> Save();
        UniTask<bool> Load(int version);
    }
}