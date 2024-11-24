using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Observers.Snake
{
    public class SnakeDeathObserver : ISnakeDeathObserver, IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly IGameUI _gameUI;

        public SnakeDeathObserver(ISnake snake, IWorldBounds worldBounds, IGameUI gameUI)
        {
            _snake = snake;
            _worldBounds = worldBounds;
            _gameUI = gameUI;
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
            _gameUI.GameOver(false);
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