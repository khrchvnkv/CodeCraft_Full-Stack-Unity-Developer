using System;
using Input;
using Modules;
using Zenject;

namespace Snake
{
    public class SnakeMovementController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IInputAdapter _inputAdapter;

        public SnakeMovementController(
            ISnake snake, 
            IInputAdapter inputAdapter)
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

        private void ChangeDirection(SnakeDirection direction) => 
            _snake.Turn(direction);
    }
}