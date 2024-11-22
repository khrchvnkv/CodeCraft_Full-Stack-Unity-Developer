using System;
using Input;
using Modules;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class SnakeController : ISnakeController, IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IInputAdapter _inputAdapter;

        public SnakeController(ISnake snake, IInputAdapter inputAdapter)
        {
            _snake = snake;
            _inputAdapter = inputAdapter;
        }

        void IInitializable.Initialize()
        {
            _inputAdapter.DirectionChanged += ChangeDirection;
        }
        
        void IDisposable.Dispose()
        {
            _inputAdapter.DirectionChanged -= ChangeDirection;
        }

        private void ChangeDirection(Vector2Int direction) => 
            _snake.Turn(GetSnakeDirection(direction));

        private static SnakeDirection GetSnakeDirection(in Vector2Int direction)
        {
            if (direction == Vector2Int.up) return SnakeDirection.UP;
            if (direction == Vector2Int.down) return SnakeDirection.DOWN;
            if (direction == Vector2Int.right) return SnakeDirection.RIGHT;
            if (direction == Vector2Int.left) return SnakeDirection.LEFT;

            throw new ArgumentException();
        }
    }
}