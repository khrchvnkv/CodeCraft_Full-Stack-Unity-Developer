using UnityEngine;
using Zenject;

namespace Coin
{
    public class CoinPool : MonoMemoryPool<Vector2Int, Modules.Coin>
    {
        protected override void Reinitialize(Vector2Int position, Modules.Coin coin)
        {
            coin.Generate();
            coin.Position = position;
        }
    }
}