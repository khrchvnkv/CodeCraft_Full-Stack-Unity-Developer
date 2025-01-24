using System;
using Game.Common;

namespace Game.App
{
    [Serializable]
    public struct ResourceBagData
    {
        public ResourceType Type { get; set; }
        
        public int Current { get; set; }
    }
}