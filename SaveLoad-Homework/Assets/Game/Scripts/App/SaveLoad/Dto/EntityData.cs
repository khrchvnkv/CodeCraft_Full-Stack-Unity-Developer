using System;
using Game.Common;

namespace Game.App
{
    [Serializable]
    public struct EntityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SerializedVector3 Position { get; set; }
        public SerializedVector3 Rotation { get; set; }
    }
}