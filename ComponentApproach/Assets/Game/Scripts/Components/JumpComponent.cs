using UnityEngine;

namespace Game.Scripts.Components
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Collider2D _triggerCollider;
        [SerializeField] private LayerMask _layerMask;

        private ICondition _condition;

        public void Construct(in ICondition condition) => _condition = condition;
        
        public void Jump()
        {
            if (_condition.Invoke() && CanJump())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce);
            }
        }

        private bool CanJump() => 
            _rigidbody.velocity.y < Mathf.Epsilon && _triggerCollider.IsTouchingLayers(_layerMask);
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}