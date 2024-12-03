using System;
using Modules;
using UnityEngine;
using Zenject;

namespace Input
{
    public class StandaloneInputAdapter : IInputAdapter, ITickable
    {
        private const SnakeDirection DefaultInputDirection = SnakeDirection.UP;

        public event Action<SnakeDirection> DirectionChanged;

        private SnakeDirection _direction = DefaultInputDirection;
        private bool _isEnabled;

        void IInputAdapter.Enable() => _isEnabled = true;

        void IInputAdapter.Disable() => _isEnabled = false;

        void ITickable.Tick()
        {
            if (!_isEnabled) return;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                ChangeDirection(SnakeDirection.UP);
            } 
            else if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                ChangeDirection(SnakeDirection.DOWN);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                ChangeDirection(SnakeDirection.LEFT);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                ChangeDirection(SnakeDirection.RIGHT);
            }
        }

        private void ChangeDirection(in SnakeDirection direction)
        {
             if (direction == _direction) return;
            
            _direction = direction;
            DirectionChanged?.Invoke(_direction);
        }
    }
}