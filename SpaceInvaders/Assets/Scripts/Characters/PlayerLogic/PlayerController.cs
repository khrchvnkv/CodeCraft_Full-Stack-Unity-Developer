using Services.Input;
using UnityEngine;

namespace Characters.PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        private static readonly Vector2 ShootingDirection = Vector2.up;
        
        [SerializeField] private Ship _player;
        [SerializeField] private PlayerInput _input;

        private bool _fireRequired;
        private float _moveDirectionX;

        private void Update()
        {
            if (!_fireRequired)
            {
                _fireRequired = _input.IsFireRequired();
            }

            _moveDirectionX = _input.GetMoveDirection();
        }

        private void FixedUpdate()
        {
            _player.Move(_moveDirectionX);
            if (_fireRequired)
            {
                _player.Shoot(ShootingDirection);
                _fireRequired = false;
            }
        }
    }
}