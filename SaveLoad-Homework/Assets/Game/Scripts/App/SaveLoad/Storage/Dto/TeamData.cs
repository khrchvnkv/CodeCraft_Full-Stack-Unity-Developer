using System;
using Game.Scripts.Common;

namespace Game.Scripts.App.SaveLoad.Storage.Dto
{
    [Serializable]
    public struct TeamData
    {
        public TeamType Type { get; set; }
    }
}