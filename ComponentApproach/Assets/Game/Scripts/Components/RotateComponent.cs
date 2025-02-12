using UnityEngine;

namespace Game.Scripts.Components
{
    public class RotateComponent : MonoBehaviour
    {
        private ICondition _condition;

        public void Construct(in ICondition condition) => _condition = condition;

        public void LookInDirection(in Vector2 direction)
        {
            if (_condition.Invoke())
            {
                var localScale = transform.localScale;
                float scaleX = Mathf.Abs(localScale.x);
                localScale = new Vector3(scaleX * FaceDirection(direction), localScale.y, localScale.z);
                transform.localScale = localScale;
            }
        }

        private float FaceDirection(in Vector2 direction) =>
            direction.x <= 0 
                ? -1 
                : 1;
        
        public interface ICondition
        {
            bool Invoke();
        }
    }
}