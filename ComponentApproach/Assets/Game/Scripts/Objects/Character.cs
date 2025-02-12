 using Game.Scripts.Components;
 using UnityEngine;

 namespace Game.Scripts.Objects
{
    public sealed class Character : MonoBehaviour, 
                                    JumpComponent.ICondition,
                                    MoveComponent.ICondition,
                                    RotateComponent.ICondition
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private JumpComponent _jumpComponent;
        [SerializeField] private HealthComponent _healthComponent;

        private void Awake() => Construct();

        private void Construct()
        {
            _moveComponent.Construct(this);
            _jumpComponent.Construct(this);
            _rotateComponent.Construct(this);
        }
        
        public void Move(in Vector2 direction)
        {
            _moveComponent.Move(direction);
            _rotateComponent.LookInDirection(direction);
        }

        public void Jump() => _jumpComponent.Jump();

        bool JumpComponent.ICondition.Invoke() => IsAlive();
        
        bool MoveComponent.ICondition.Invoke() => IsAlive();
        
        bool RotateComponent.ICondition.Invoke() => IsAlive();

        private bool IsAlive() => _healthComponent.IsAlive;
    }
}