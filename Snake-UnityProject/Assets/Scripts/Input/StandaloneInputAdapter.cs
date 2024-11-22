using System;
using UnityEngine;
using Zenject;

namespace Input
{
    public class StandaloneInputAdapter : IInputAdapter, ITickable
    {
        public event Action<Vector2Int> DirectionChanged;

        private Vector2Int _direction = Vector2Int.zero;

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