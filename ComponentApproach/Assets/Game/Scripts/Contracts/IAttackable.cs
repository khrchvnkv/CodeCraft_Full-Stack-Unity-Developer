using UnityEngine;

namespace Game.Scripts.Contracts
{
    public interface IAttackable
    {
        void Attack(IDamageable damageable, Rigidbody2D rb);
    }
}