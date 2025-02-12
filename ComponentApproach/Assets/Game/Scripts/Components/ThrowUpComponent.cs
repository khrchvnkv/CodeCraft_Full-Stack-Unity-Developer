using UnityEngine;

namespace Game.Scripts.Components
{
    public class ThrowUpComponent
    {
        private readonly ICondition _condition;
        private readonly float _force;

        public ThrowUpComponent(ICondition condition, float force)
        {
            _condition = condition;
            _force = force;
        }

        public void ThrowUp(in Rigidbody2D rigidbody)
        {
            if (_condition.Invoke())
            {
                rigidbody.AddForce(Vector2.up * _force);
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}