using UnityEngine;

namespace Game.Scripts.Components
{
    public class PushComponent
    {
        private readonly ICondition _condition;
        private readonly float _force;

        public PushComponent(
            ICondition condition,
            float force)
        {
            _condition = condition;
            _force = force;
        }
        
        public void Push(in Rigidbody2D rigidbody, in Vector2 direction)
        {
            if (_condition.Invoke())
            {
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(direction.normalized * _force);
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}