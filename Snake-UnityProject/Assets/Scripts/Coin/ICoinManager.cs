using System;
using UnityEngine;

namespace Coin
{
    public interface ICoinManager
    {
        event Action OnAllCoinsCollected;

        void SpawnCoins(in int count);
        bool CanCollectCoin(Vector2Int position, out int score, out int bones);
        void RemoveCoinAtPosition(in Vector2Int position);
    }
}