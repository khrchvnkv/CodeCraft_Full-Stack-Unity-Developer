using UnityEngine;

namespace Game.Scripts.Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _movementSpeed;

        private ICondition _condition;

        public void Construct(in ICondition condition) => _condition = condition;

        public void Move(in Vector2 direction)
        {
            if (_condition.Invoke())
            {
                _rigidbody.velocity = new Vector2(direction.x * _movementSpeed, _rigidbody.velocity.y);
            }
        }
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}