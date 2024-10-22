using Bullets;
using Common;
using Services.Input;
using UnityEngine;

namespace Characters.PlayerLogic
{
    [RequireComponent(typeof(InputController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _character;
        [SerializeField] private InputController _inputController;
        
        [SerializeField] private float _movementSpeed;
        
        private bool _fireRequired;
        private float _moveDirection;

        private void OnValidate() => _inputController ??= gameObject.GetComponent<InputController>();

        private void OnEnable() => _character.OnHealthEmpty += StopSimulation;
        
        private void OnDisable() => _character.OnHealthEmpty -= StopSimulation;

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
            _character.Move(moveDirection);
        }
        
        private void Shoot()
        {
            if (!_fireRequired) return;
            
            _character.SpawnBullet();

            _fireRequired = false;
        }
        
        private void StopSimulation(Player _)
        {
            Time.timeScale = 0;
        }
    }
}