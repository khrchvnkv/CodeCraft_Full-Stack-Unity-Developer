using System;
using Modules;
using Zenject;

namespace Snake
{
    public class SnakeDifficultyController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IDifficulty _difficulty;

        public SnakeDifficultyController(
            ISnake snake, 
            IDifficulty difficulty)
        {
            _snake = snake;
            _difficulty = difficulty;
        }

        void IInitializable.Initialize()
        {
            _difficulty.OnStateChanged += UpdateSpeed;
        }

        void IDisposable.Dispose()
        {
            _difficulty.OnStateChanged -= UpdateSpeed;
        }

        private void UpdateSpeed() => _snake.SetSpeed(_difficulty.Current);
    }
}