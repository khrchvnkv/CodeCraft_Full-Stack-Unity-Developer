using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class ThrowUpComponent
    {
        private readonly ICondition _condition;
        private readonly float _force;
        private readonly Vector2 _direction;

        public event Action ThrowedUp;
        public event Action EmptyThrowed;
        
        public ThrowUpComponent(
            ICondition condition, 
            float force,
            Vector2 direction)
        {
            _condition = condition;
            _force = force;
            _direction = direction;
        }

        public void ThrowUp(in Rigidbody2D rigidbody)
        {
            if (_condition.Invoke())
            {
                var velocity = rigidbody.velocity;
                velocity.y = 0;

                rigidbody.velocity = velocity;
                rigidbody.AddForce(_direction * _force);
                ThrowedUp?.Invoke();
            }
        }

        public void ThrowUp(in IReadOnlyCollection<Rigidbody2D> collection)
        {
            if (_condition.Invoke())
            {
                if (collection.Count > 0)
                {
                    foreach (var body in collection)
                    {
                        ThrowUp(body);
                    }
                }
                else
                {
                    EmptyThrowed?.Invoke();
                }
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}