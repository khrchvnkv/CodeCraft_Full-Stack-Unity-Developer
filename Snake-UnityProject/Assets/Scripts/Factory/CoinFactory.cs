using System.Collections.Generic;
using Controllers.Snake.CoinCollecting;
using Modules;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class CoinFactory : MonoMemoryPool<Vector2Int, Coin>, ICoinFactory, ICoinCollision
    {
        private readonly Dictionary<Vector2Int, Coin> _createdCoins = new();

        ICoin ICoinFactory.Create(in Vector2Int position) => Spawn(position);

        void ICoinFactory.Remove(in Coin coin) => Despawn(coin);

        bool ICoinCollision.IsCoinCollided(Vector2Int position, out Coin coin) =>
            _createdCoins.TryGetValue(position, out coin);

        bool ICoinCollision.AllCoinsCollected() => _createdCoins.Count == 0;

        protected override void Reinitialize(Vector2Int position, Coin coin)
        {
            coin.Generate();
            coin.Position = position;
            _createdCoins.TryAdd(position, coin);
        }

        protected override void OnDespawned(Coin coin)
        {
            base.OnDespawned(coin);
            _createdCoins.Remove(coin.Position);
        }
    }
}