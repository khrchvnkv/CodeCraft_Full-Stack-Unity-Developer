using System;

namespace Game.UI
{
    //Don't modify
    public interface IControlsPresenter
    {
        void Save(Action<bool, int> callback);
        void Load(string version, Action<bool, int> callback);
    }
}