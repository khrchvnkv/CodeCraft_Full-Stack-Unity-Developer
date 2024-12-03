using UnityEngine;

namespace Coin
{
    public interface ICoinManager
    {
        void SpawnCoins(in int count);
        void Remove(in Modules.Coin coin);
        bool IsCoinCollided(Vector2Int position, out Modules.Coin coin);
        bool AllCoinsCollected();
    }
}