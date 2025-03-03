using System;
using System.Collections.Generic;
using Modules.Entity;
using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class TossComponent
    {
        private readonly ICondition _condition;
        private readonly float _force;
        private readonly Vector2 _direction;

        public event Action Tossed;
        public event Action EmptyTossed;
        
        public TossComponent(
            ICondition condition, 
            float force,
            Vector2 direction)
        {
            _condition = condition;
            _force = force;
            _direction = direction;
        }

        public void Toss(in Entity entity)
        {
            if (entity.TryGet(out Rigidbody2D rigidbody2D))
            {
                Toss(rigidbody2D);
            }
        }

        public void Toss(in Rigidbody2D rigidbody)
        {
            if (_condition.Invoke())
            {
                TossAction(rigidbody);
                Tossed?.Invoke();
            }
        }

        public void Toss(in IReadOnlyCollection<Rigidbody2D> collection)
        {
            if (_condition.Invoke())
            {
                if (collection.Count > 0)
                {
                    foreach (var body in collection)
                    {
                        TossAction(body);
                    }
                    Tossed?.Invoke();
                }
                else
                {
                    EmptyTossed?.Invoke();
                }
            }
        }

        private void TossAction(in Rigidbody2D rigidbody)
        {
            var velocity = rigidbody.velocity;
            velocity.y = 0;

            rigidbody.velocity = velocity;
            rigidbody.AddForce(_direction * _force);
        }

        public interface ICondition
        {
            bool Invoke();
        }
    }
}