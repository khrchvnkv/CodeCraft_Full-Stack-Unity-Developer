using Game.Scripts.Objects;
using UnityEngine;

namespace Game.Scripts.Controllers
{
    public class CharacterController : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        
        [SerializeField] private Character _character;
        
        private Vector2 _movementInput;
        private bool _isJumpRequest;
        
        private bool HasInput => _movementInput.sqrMagnitude > Mathf.Epsilon * Mathf.Epsilon;
        
        private void Update() => ReadMovementInput();

        private void FixedUpdate() => HandleMovementInput();

        private void ReadMovementInput()
        {
            var horizontal = Input.GetAxis(HorizontalAxis);
            _movementInput = new Vector2(horizontal, 0);

            if (Input.GetKeyDown(KeyCode.Space) && !_isJumpRequest)
            {
                _isJumpRequest = true;
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
        }
    }
}