using System;
using Modules;

namespace Input
{
    public interface IInputAdapter
    {
        event Action<SnakeDirection> DirectionChanged;

        void Enable();
        void Disable();
    }
}