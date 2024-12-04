using System;
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

        public event Action OnAllCoinsCollected;

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

        bool ICoinManager.CanCollectCoin(Vector2Int position, out int score, out int bones)
        {
            score = default;
            bones = default;

            if (_createdCoins.TryGetValue(position, out var coin))
            {
                score = coin.Score;
                bones = coin.Bones;
                
                return true;
            }

            return false;
        }

        void ICoinManager.RemoveCoinAtPosition(in Vector2Int position)
        {
            if (_createdCoins.TryGetValue(position, out var coin))
            {
                Remove(coin);
            }
            else
            {
                Debug.LogError($"No coins at position {position}");
            }
        }
        
        private ICoin Create(in Vector2Int position)
        {
            var coin = _coinPool.Spawn(position);
            _createdCoins.Add(position, coin);
            return coin;
        }

        private void Remove(in Modules.Coin coin)
        {
            _createdCoins.Remove(coin.Position);
            _coinPool.Despawn(coin);

            if (_createdCoins.Count == 0)
            {
                OnAllCoinsCollected?.Invoke();
            }
        }
    }
}