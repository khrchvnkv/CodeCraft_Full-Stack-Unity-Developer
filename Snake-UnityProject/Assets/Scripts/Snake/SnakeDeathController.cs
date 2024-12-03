using System;
using GameCycle;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Snake
{
    public class SnakeDeathController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly IGameCycle _gameCycle;

        public SnakeDeathController(
            ISnake snake, 
            IWorldBounds worldBounds, 
            IGameCycle gameCycle)
        {
            _snake = snake;
            _worldBounds = worldBounds;
            _gameCycle = gameCycle;
        }

        void IInitializable.Initialize()
        {
            _snake.OnSelfCollided += SnakeDied;
            _snake.OnMoved += SnakeMoved;
        }

        void IDisposable.Dispose()
        {
            _snake.OnSelfCollided -= SnakeDied;
            _snake.OnMoved -= SnakeMoved;
        }

        private void SnakeDied()
        {
            _snake.SetActive(false);
            _gameCycle.LossGame();
        }

        private void SnakeMoved(Vector2Int position)
        {
            if (!_worldBounds.IsInBounds(position))
            {
                SnakeDied();
            }
        }
    }
}