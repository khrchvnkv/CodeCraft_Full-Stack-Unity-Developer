using System;
using Modules;
using Zenject;

namespace Coin
{
    public class CoinsDifficultyController :  IInitializable, IDisposable
    {
        private readonly ICoinManager _coinManager;
        private readonly IDifficulty _difficulty;

        public CoinsDifficultyController(
            ICoinManager coinManager, 
            IDifficulty difficulty)
        {
            _coinManager = coinManager;
            _difficulty = difficulty;
        }

        void IInitializable.Initialize() => _difficulty.OnStateChanged += SpawnCoins;

        void IDisposable.Dispose() => _difficulty.OnStateChanged -= SpawnCoins;

        private void SpawnCoins()
        {
            var count = _difficulty.Current + 1;
            _coinManager.SpawnCoins(count);
        }
    }
}