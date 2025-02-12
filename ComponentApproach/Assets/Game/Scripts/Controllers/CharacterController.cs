using System.Collections.Generic;
using Game.Scripts.Objects;
using Game.Scripts.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class CharacterController : ITickable, IFixedTickable
    {
        private const string HorizontalAxis = "Horizontal";
        
        private readonly Character _character;
        private readonly Rigidbody2D _rigidbody;
        private readonly PushTrigger _pushTrigger;

        private Vector2 _movementInput;
        private bool _isJumpRequest;
        private bool _isPushRequest;
        private bool _isThrowUpRequest;

        private bool HasInput => _movementInput.sqrMagnitude > Mathf.Epsilon * Mathf.Epsilon;

        public CharacterController(
            Character character, 
            Rigidbody2D rigidbody,
            PushTrigger pushTrigger)
        {
            _character = character;
            _rigidbody = rigidbody;
            _pushTrigger = pushTrigger;
        }

        void ITickable.Tick() => ReadMovementInput();

        void IFixedTickable.FixedTick() => HandleMovementInput();

        private void ReadMovementInput()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            _movementInput = new Vector2(horizontal, 0);

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumpRequest)
            {
                _isJumpRequest = true;
            }
            
            if (Input.GetMouseButtonDown(0) && !_isPushRequest)
            {
                _isPushRequest = true;
            }
            
            if (Input.GetMouseButtonDown(1) && !_isThrowUpRequest)
            {
                _isThrowUpRequest = true;
            }
        }

        private void HandleMovementInput()
        {
            if (HasInput)
            {
                _character.Move(_movementInput);
            }

            if (_isJumpRequest)
            {
                _character.Jump();
                _isJumpRequest = false;  
            }

            PushOrThrowUp();
        }

        private void PushOrThrowUp()
        {
            if (!_isPushRequest && !_isThrowUpRequest)
            {
                return;
            }

            var targets = GetTargetsCollection();
            if (targets != null)
            {
                foreach (var target in targets)
                {
                    if (_isPushRequest)
                    {
                        var direction = target.position - _rigidbody.position;
                        _character.Push(target, direction);
                    }

                    if (_isThrowUpRequest)
                    {
                        _character.ThrowUp(target);
                    }  
                }
            }

            _isPushRequest = false;
            _isThrowUpRequest = false;
        }

        private IEnumerable<Rigidbody2D> GetTargetsCollection() => _pushTrigger.Targets;
    }
}