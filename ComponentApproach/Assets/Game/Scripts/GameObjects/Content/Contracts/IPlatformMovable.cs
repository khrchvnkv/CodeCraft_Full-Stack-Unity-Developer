using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Contracts
{
    public interface IPlatformMovable
    {
        Rigidbody2D Rigidbody { get; }
    }
}