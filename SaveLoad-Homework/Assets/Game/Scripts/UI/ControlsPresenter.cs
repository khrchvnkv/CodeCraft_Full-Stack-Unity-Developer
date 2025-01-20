using System;
using Cysharp.Threading.Tasks;
using Game.Scripts.App.SaveLoad;

namespace Game.Scripts.UI
{
    public sealed class ControlsPresenter : IControlsPresenter
    {
        private readonly ISaveLoader _saveLoader;

        public ControlsPresenter(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
        }

        public void Save(Action<bool, int> callback)
        {
            SaveAsync().Forget();
            
            async UniTask SaveAsync()
            {
                var (result, version) = await _saveLoader.Save();
                callback?.Invoke(result, version);
            }
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            if (int.TryParse(versionText, out var version))
            {
                LoadAsync().Forget();
            }
            else
            {
                const int unknownVersion = -1;
                callback?.Invoke(false, unknownVersion);
            }

            async UniTask LoadAsync()
            {
                var result = await _saveLoader.Load(version);
                callback?.Invoke(result, version);
            }
        }
    }
}