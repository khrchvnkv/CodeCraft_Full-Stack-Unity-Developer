using System.Collections.Generic;
using Modules;
using SnakeGame;
using UnityEngine;

namespace Coin
{
    public class CoinManager : ICoinManager
    {
        private readonly Dictionary<Vector2Int, Modules.Coin> _createdCoins = new();
        private readonly CoinPool _coinPool;
        private readonly IWorldBounds _worldBounds;

        public CoinManager(
            CoinPool coinPool, 
            IWorldBounds worldBounds)
        {
            _coinPool = coinPool;
            _worldBounds = worldBounds;
        }

        void ICoinManager.SpawnCoins(in int count)
        {
            for (int i = 0; i < count; i++)
            {
                Create(_worldBounds.GetRandomPosition());
            }
        }

        void ICoinManager.Remove(in Modules.Coin coin)
        {
            _createdCoins.Remove(coin.Position);
            _coinPool.Despawn(coin);
        }

        bool ICoinManager.IsCoinCollided(Vector2Int position, out Modules.Coin coin) =>
            _createdCoins.TryGetValue(position, out coin);

        bool ICoinManager.AllCoinsCollected() => _createdCoins.Count == 0;

        private ICoin Create(in Vector2Int position)
        {
            var coin = _coinPool.Spawn(position);
            _createdCoins.Add(position, coin);
            return coin;
        }
    }
}