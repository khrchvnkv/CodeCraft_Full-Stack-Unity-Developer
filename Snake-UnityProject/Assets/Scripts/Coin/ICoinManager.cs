using System;
using UnityEngine;

namespace Coin
{
    public interface ICoinManager
    {
        event Action OnAllCoinsCollected;

        void SpawnCoins(in int count);
        bool TryTakeCoin(Vector2Int position, out int score, out int bones);
    }
}