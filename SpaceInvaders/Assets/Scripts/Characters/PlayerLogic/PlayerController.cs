using Services.Input;
using UnityEngine;

namespace Characters.PlayerLogic
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player _player;
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
                _player.Shoot();
                _fireRequired = false;
            }
        }
    }
}