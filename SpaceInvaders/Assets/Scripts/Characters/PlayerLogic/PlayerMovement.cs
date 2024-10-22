using UnityEngine;

namespace Characters.PlayerLogic
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        
        public Vector2 Position => _rigidbody.position;

        public void Move(Vector2 direction)
        {
            Vector2 moveStep = direction * (_speed * Time.fixedDeltaTime);
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }
    }
}