using System;
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

        public CoinCollectingController(
            ICoinManager coinManager, 
            ISnake snake, 
            IScore score)
        {
            _coinManager = coinManager;
            _snake = snake;
            _score = score;
        }

        void IInitializable.Initialize() => _snake.OnMoved += CheckCollection;

        void IDisposable.Dispose() => _snake.OnMoved -= CheckCollection;

        private void CheckCollection(Vector2Int position)
        {
            if (_coinManager.CanCollectCoin(position, out var score, out var bones))
            {
                _score.Add(score);
                _snake.Expand(bones);
                _coinManager.RemoveCoinAtPosition(position);
            }
        }
    }
}