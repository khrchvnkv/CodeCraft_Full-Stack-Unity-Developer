using Characters.Common;
using UnityEngine;

namespace Characters.PlayerLogic
{
    public sealed class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerShooting _shooting;
        [SerializeField] private MovementComponent _movement;
        [SerializeField] private HealthComponent _health;

        public bool IsAlive => _health.IsAlive;
        public Vector2 Position => _movement.Position;

        private void Awake() => _health.ResetHealth();

        private void OnEnable() => _health.OnHealthEmpty += StopSimulation;
        
        private void OnDisable() => _health.OnHealthEmpty -= StopSimulation;

        public void Move(in float moveDirectionX)
        {
            Vector2 moveDirection = new Vector2(moveDirectionX, 0);
            _movement.Move(moveDirection);
        }
        
        public void Shoot() => _shooting.Shoot();

        void IDamageable.DealDamage(in int damage) => _health.DealDamage(damage);

        private void StopSimulation() => Time.timeScale = 0;
    }
}