using System;
using UnityEngine;

namespace Characters.EnemyLogic.AIAgents
{
    [Serializable]
    public class MovementAgent
    {
        private Ship _ship;
        private Vector2 _destination;

        private Vector2 Position => _ship.Position;
        
        public void Construct(
            in Ship ship,
            in Vector2 startPoint,
            in Vector2 endPoint)
        {
            _ship = ship;
            _ship.SetPosition(startPoint);
            _destination = endPoint;
        }
        
        public bool TryMove()
        {
            Vector2 vector = _destination - Position;
            if (vector.magnitude <= 0.25f)
            {
                return false;
            }

            vector = Vector2.ClampMagnitude(vector, 1.0f);
            _ship.Move(vector);
            return true;
        }
    }
}