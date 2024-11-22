using Modules;
using UnityEngine;

namespace Factory.Coin
{
    public interface ICoinFactory
    {
        ICoin Create(Vector2Int position);
        void Remove(Modules.Coin coin);
    }
}