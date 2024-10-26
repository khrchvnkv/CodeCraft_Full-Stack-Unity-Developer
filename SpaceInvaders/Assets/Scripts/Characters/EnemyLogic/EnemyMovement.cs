using System;
using Characters.Common;
using UnityEngine;

namespace Characters.EnemyLogic
{
    [Serializable]
    public class EnemyMovement : MovementComponent
    {
        private Vector2 _destination;

        public void Construct(
            in Vector2 startPoint,
            in Vector2 endPoint)
        {
            SetPosition(startPoint);
            _destination = endPoint;
        }
        
        public bool TryMove()
        {
            Vector2 vector = _destination - Position;
            if (vector.magnitude <= 0.25f)
            {
                return false;
            }

            Move(vector);
            return true;
        }
    }
}