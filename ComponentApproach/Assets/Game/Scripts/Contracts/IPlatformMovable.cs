using UnityEngine;

namespace Game.Scripts.Contracts
{
    public interface IPlatformMovable
    {
        Rigidbody2D Rigidbody { get; }
    }
}