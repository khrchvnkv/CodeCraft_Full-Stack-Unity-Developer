using System;
using Game.Scripts.Common;

namespace Game.Scripts.App.SaveLoad.Storage.Dto
{
    [Serializable]
    public struct DestinationPointData
    {
        public SerializedVector3 Value { get; set; }
    }
}