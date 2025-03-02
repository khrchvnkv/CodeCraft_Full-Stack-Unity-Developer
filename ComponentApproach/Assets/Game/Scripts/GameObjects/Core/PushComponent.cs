using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GameObjects.Core
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
                PushAction(rigidbody, direction);
                Pushed?.Invoke();
            }
        }

        public void Push(in KeyValuePair<Rigidbody2D, Vector2>[] collection)
        {
            if (_condition.Invoke())
            {
                if (collection.Length > 0)
                {
                    foreach (var (body, direction) in collection)
                    {
                        PushAction(body, direction);
                    }
                    
                    Pushed?.Invoke();
                }
                else
                {
                    EmptyPush();
                }
            }
        }

        private void PushAction(in Rigidbody2D body, in Vector2 direction)
        {
            body.velocity = Vector2.zero;
            body.AddForce(direction.normalized * _force);
        }

        private void EmptyPush() => EmptyPushed?.Invoke();

        public interface ICondition
        {
            bool Invoke();
        }
    }
}