using System.Collections.Generic;
using Collision.Contracts;
using Modules;
using UnityEngine;
using Zenject;

namespace Factory.Coin
{
    public class CoinFactory : MonoMemoryPool<Vector2Int, Modules.Coin>, ICoinFactory, ICoinCollision
    {
        private readonly Dictionary<Vector2Int, Modules.Coin> _createdCoins;

        ICoin ICoinFactory.Create(Vector2Int position) => Spawn(position);

        void ICoinFactory.Remove(Modules.Coin coin) => Despawn(coin);

        bool ICoinCollision.IsCoinCollided(Vector2Int position, out Modules.Coin coin) =>
            _createdCoins.TryGetValue(position, out coin);

        protected override void Reinitialize(Vector2Int position, Modules.Coin coin)
        {
            coin.Generate();
            coin.Position = position;
        }

        protected override void OnSpawned(Modules.Coin coin)
        {
            base.OnSpawned(coin);
            _createdCoins.TryAdd(coin.Position, coin);
        }

        protected override void OnDespawned(Modules.Coin coin)
        {
            base.OnDespawned(coin);
            _createdCoins.Remove(coin.Position);
        }
    }
}