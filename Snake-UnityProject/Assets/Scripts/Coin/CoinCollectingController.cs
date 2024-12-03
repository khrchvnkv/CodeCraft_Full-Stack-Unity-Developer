using System;
using GameCycle;
using Modules;
using UnityEngine;
using Zenject;

namespace Coin
{
    public class CoinCollectingController : IInitializable, IDisposable
    {
        private readonly ICoinManager _coinManager;
        private readonly ISnake _snake;
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;
        private readonly IGameCycle _gameCycle;

        private bool IsGameCompleted => _coinManager.AllCoinsCollected() && !_difficulty.Next(out _);
        
        public CoinCollectingController(
            ICoinManager coinManager, 
            ISnake snake, 
            IScore score, 
            IDifficulty difficulty, 
            IGameCycle gameCycle)
        {
            _coinManager = coinManager;
            _snake = snake;
            _score = score;
            _difficulty = difficulty;
            _gameCycle = gameCycle;
        }

        void IInitializable.Initialize() => _snake.OnMoved += CheckCollection;

        void IDisposable.Dispose() => _snake.OnMoved -= CheckCollection;

        private void CheckCollection(Vector2Int position)
        {
            if (_coinManager.IsCoinCollided(position, out var coin))
            {
                CollectCoin(coin);
            }
        }

        private void CollectCoin(in Modules.Coin coin)
        {
            _score.Add(coin.Score);
            _snake.Expand(coin.Bones);
            _coinManager.Remove(coin);

            if (IsGameCompleted)
            {
                _snake.SetActive(false);
                _gameCycle.CompleteGame();
            }
        }
    }
}