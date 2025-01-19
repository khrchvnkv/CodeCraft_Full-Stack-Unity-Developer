using System;
using Game.Scripts.Common;

namespace Game.Scripts.App.SaveLoad.Storage.Dto
{
    [Serializable]
    public struct ResourceBagData
    {
        public ResourceType Type { get; set; }
        
        public int Current { get; set; }
    }
}