using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Contracts
{
    public interface IAttackable
    {
        void Attack(IDamageable damageable, Rigidbody2D rb);
    }
}