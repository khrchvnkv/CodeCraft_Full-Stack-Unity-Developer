using UnityEngine;

namespace Game.Scripts.Components
{
    public class GroundDetectComponent : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private LayerMask _layerMask;

        public bool IsGround() => 
            _collider.IsTouchingLayers(_layerMask);
    }
}