using System;
using Input;
using Modules;
using UnityEngine;
using Zenject;

namespace Controllers.Snake.Movement
{
    public class SnakeMovementController : ISnakeMovementController, IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IInputAdapter _inputAdapter;
        private readonly IDifficulty _difficulty;

        public SnakeMovementController(ISnake snake, IInputAdapter inputAdapter, IDifficulty difficulty)
        {
            _snake = snake;
            _inputAdapter = inputAdapter;
            _difficulty = difficulty;
        }

        void IInitializable.Initialize()
        {
            _inputAdapter.DirectionChanged += ChangeDirection;
            _difficulty.OnStateChanged += UpdateSpeed;
        }

        void IDisposable.Dispose()
        {
            _inputAdapter.DirectionChanged -= ChangeDirection;
            _difficulty.OnStateChanged -= UpdateSpeed;
        }

        private void UpdateSpeed() => _snake.SetSpeed(_difficulty.Current);

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