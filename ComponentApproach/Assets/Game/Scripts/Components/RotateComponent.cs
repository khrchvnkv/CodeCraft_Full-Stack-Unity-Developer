using UnityEngine;

namespace Game.Scripts.Components
{
    public class RotateComponent
    {
        private readonly ICondition _condition;
        private readonly Transform _transform;

        public float LookDirection { get; private set; }

        public RotateComponent(
            ICondition condition,
            Transform transform)
        {
            _condition = condition;
            _transform = transform;
        }

        public void LookInDirection(in Vector2 direction)
        {
            if (_condition.Invoke())
            {
                LookDirection = FaceDirection(direction);
                var localScale = _transform.localScale;
                float scaleX = Mathf.Abs(localScale.x);
                localScale = new Vector3(scaleX * LookDirection, localScale.y, localScale.z);
                _transform.localScale = localScale;
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