using System;
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
            var result = _saveLoader.Save(out var version);
            callback?.Invoke(result, version);
        }

        public void Load(string versionText, Action<bool, int> callback)
        {
            if (int.TryParse(versionText, out var version))
            {
                var result = _saveLoader.Load(version);
                callback?.Invoke(result, version);
            }
            else
            {
                const int unknownVersion = -1;
                callback?.Invoke(false, unknownVersion);
            }
        }
    }
}