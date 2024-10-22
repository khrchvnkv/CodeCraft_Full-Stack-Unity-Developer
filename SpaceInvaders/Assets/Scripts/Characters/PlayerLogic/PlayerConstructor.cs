using Services.Input;
using UnityEngine;

namespace Characters.PlayerLogic
{
    [RequireComponent(
        typeof(PlayerShooting),
        typeof(PlayerMovement),
        typeof(HealthComponent))]
    public sealed class PlayerConstructor : MonoBehaviour
    {
        [SerializeField] private PlayerShooting _shooting;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private HealthComponent _health;
        [SerializeField] private InputController _inputController;
        
        private bool _fireRequired;
        private float _moveDirection;

        public bool IsAlive => _health.IsAlive;
        public Vector2 Position => _movement.Position;

        private void OnValidate()
        {
            _shooting ??= gameObject.GetComponent<PlayerShooting>();
            _movement ??= gameObject.GetComponent<PlayerMovement>();
            _health ??= gameObject.GetComponent<HealthComponent>();
        }

        private void Awake() => _health.ResetHealth();

        private void OnEnable() => _health.OnHealthEmpty += StopSimulation;
        
        private void OnDisable() => _health.OnHealthEmpty -= StopSimulation;

        private void Update()
        {
            if (_inputController.IsFireRequired())
            {
                _fireRequired = true;
            }
            
            _moveDirection = _inputController.GetMoveDirection();
        }
        
        private void FixedUpdate()
        {
            Move();
            Shoot();
        }

        private void Move()
        {
            Vector2 moveDirection = new Vector2(_moveDirection, 0);
            _movement.Move(moveDirection);
        }
        
        private void Shoot()
        {
            if (!_fireRequired) return;
            
            _shooting.Shoot();

            _fireRequired = false;
        }
        
        private void StopSimulation() => Time.timeScale = 0;
    }
}