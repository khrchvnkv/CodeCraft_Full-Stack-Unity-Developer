using System;
using Factory;
using Modules;
using SnakeGame;
using Zenject;

namespace Controllers.Coin.Spawn
{
    public class CoinsStateController : ICoinsStateController, IInitializable, IDisposable
    {
        private readonly ICoinFactory _coinFactory;
        private readonly IDifficulty _difficulty;
        private readonly IWorldBounds _worldBounds;

        public CoinsStateController(ICoinFactory coinFactory, IDifficulty difficulty, IWorldBounds worldBounds)
        {
            _coinFactory = coinFactory;
            _difficulty = difficulty;
            _worldBounds = worldBounds;
        }

        void IInitializable.Initialize()
        {
            _difficulty.OnStateChanged += SpawnCoins;
            SpawnCoins();
        }
        
        void IDisposable.Dispose()
        {
            _difficulty.OnStateChanged -= SpawnCoins;
        }

        private void SpawnCoins()
        {
            var difficulty = _difficulty.Current;
            for (int i = 0; i < difficulty; i++)
            {
                _coinFactory.Create(_worldBounds.GetRandomPosition());
            }
        }
    }
}