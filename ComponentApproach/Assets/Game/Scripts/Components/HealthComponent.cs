using System;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _hp;

        public bool IsAlive => _hp > 0;

        public void TakeDamage(in int damage)
        {
            _hp = Math.Max(0, _hp - damage);
        }
    }
}