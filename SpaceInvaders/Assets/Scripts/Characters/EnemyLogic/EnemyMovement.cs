using UnityEngine;

namespace Characters.EnemyLogic
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;

        private Vector2 _destination;

        public void Construct(
            in Vector2 startPoint,
            in Vector2 endPoint)
        {
            _rigidbody.MovePosition(startPoint);
            _destination = endPoint;
        }
        
        public bool TryMove()
        {
            Vector2 vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _rigidbody.MovePosition(_destination);
                return false;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            Vector2 nextPosition = _rigidbody.position + direction * _speed;
            _rigidbody.MovePosition(nextPosition);
            return true;
        }
    }
}