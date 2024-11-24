using System;
using UnityEngine;
using Zenject;

namespace Input
{
    public class StandaloneInputAdapter : IInputAdapter, ITickable
    {
        private static readonly Vector2Int DefaultInputDirection = Vector2Int.zero;
        
        public event Action<Vector2Int> DirectionChanged;

        private Vector2Int _direction = DefaultInputDirection;

        void IInputAdapter.Reset() => _direction = DefaultInputDirection;

        void ITickable.Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                ChangeDirection(Vector2Int.up);
            } 
            else if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                ChangeDirection(Vector2Int.down);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            {
                ChangeDirection(Vector2Int.left);
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            {
                ChangeDirection(Vector2Int.right);
            }
        }

        private void ChangeDirection(in Vector2Int direction)
        {
            if (direction == _direction) return;
            
            _direction = direction;
            DirectionChanged?.Invoke(_direction);
        }
    }
}