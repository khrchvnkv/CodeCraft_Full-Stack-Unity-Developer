using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Components
{
    public class PushComponent
    {
        private readonly ICondition _condition;
        private readonly float _force;

        public event Action Pushed;
        public event Action EmptyPushed;

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
                Pushed?.Invoke();
            }
        }

        public void Push(in IReadOnlyCollection<Rigidbody2D> collection, in Vector2 fromCenter)
        {
            if (_condition.Invoke())
            {
                if (collection.Count > 0)
                {
                    foreach (var body in collection)
                    {
                        var direction = body.position - fromCenter;
                        Push(body, direction);
                    }
                }
                else
                {
                    EmptyPushed?.Invoke();
                }
            }
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}