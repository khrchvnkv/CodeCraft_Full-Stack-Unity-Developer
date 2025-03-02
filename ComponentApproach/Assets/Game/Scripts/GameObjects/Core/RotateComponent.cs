using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class RotateComponent
    {
        private readonly ICondition _condition;
        private readonly Transform[] _transforms;
        private float LookDirection { get; set; }

        public RotateComponent(
            ICondition condition,
            Transform[] transforms)
        {
            _condition = condition;
            _transforms = transforms;
        }

        public void LookInDirection(in Vector2 direction)
        {
            if (direction == Vector2.zero)
            {
                return;
            }
            
            if (_condition.Invoke())
            {
                LookDirection = FaceDirection(direction);
                foreach (var transform in _transforms)
                {
                    var localScale = transform.localScale;
                    float scaleX = Mathf.Abs(localScale.x);
                    localScale = new Vector3(scaleX * LookDirection, localScale.y, localScale.z);
                    transform.localScale = localScale;
                }
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