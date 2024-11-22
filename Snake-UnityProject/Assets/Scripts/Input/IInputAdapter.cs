using System;
using UnityEngine;

namespace Input
{
    public interface IInputAdapter
    {
        event Action<Vector2Int> DirectionChanged;
    }
}