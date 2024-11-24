using Modules;
using UnityEngine;

namespace Factory
{
    public interface ICoinFactory
    {
        ICoin Create(in Vector2Int position);
        void Remove(in Coin coin);
    }
}