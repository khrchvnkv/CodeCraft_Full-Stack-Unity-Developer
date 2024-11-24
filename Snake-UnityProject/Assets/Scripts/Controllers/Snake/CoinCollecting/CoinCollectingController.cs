using System;
using Factory;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Controllers.Snake.CoinCollecting
{
    public class CoinCollectingController : ICoinCollectingController, IInitializable, IDisposable
    {
        private readonly ICoinCollision _coinCollision;
        private readonly ISnake _snake;
        private readonly ICoinFactory _coinFactory;
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;
        private readonly IGameUI _gameUI;

        public CoinCollectingController(ICoinCollision coinCollision, 
            ISnake snake, ICoinFactory coinFactory, IScore score, 
            IDifficulty difficulty, IGameUI gameUI)
        {
            _coinCollision = coinCollision;
            _snake = snake;
            _coinFactory = coinFactory;
            _score = score;
            _difficulty = difficulty;
            _gameUI = gameUI;
        }

        void IInitializable.Initialize()
        {
            _snake.OnMoved += CheckCollection;
            _difficulty.Next(out _);
        }

        void IDisposable.Dispose()
        {
            _snake.OnMoved -= CheckCollection;
        }

        private void CheckCollection(Vector2Int position)
        {
            if (_coinCollision.IsCoinCollided(position, out var coin))
            {
                _score.Add(coin.Score);
                _snake.Expand(coin.Bones);
                _coinFactory.Remove(coin);

                if (_coinCollision.AllCoinsCollected() && !_difficulty.Next(out _))
                {
                    _snake.SetActive(false);
                    _gameUI.GameOver(true);
                }
            }
        }
    }
}