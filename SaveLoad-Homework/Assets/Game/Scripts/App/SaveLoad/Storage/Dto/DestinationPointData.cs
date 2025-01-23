using System;
using Game.Common;

namespace Game.App
{
    [Serializable]
    public struct DestinationPointData
    {
        public SerializedVector3 Value { get; set; }
    }
}