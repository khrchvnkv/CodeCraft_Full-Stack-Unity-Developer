using UnityEngine;

namespace Controllers.Snake.CoinCollecting
{
    public interface ICoinCollision
    {
        bool IsCoinCollided(Vector2Int position, out Modules.Coin coin);
        bool AllCoinsCollected();
    }
}