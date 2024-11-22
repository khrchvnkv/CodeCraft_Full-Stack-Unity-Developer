using UnityEngine;

namespace Collision.Contracts
{
    public interface ICoinCollision
    {
        bool IsCoinCollided(Vector2Int position, out Modules.Coin coin);
    }
}