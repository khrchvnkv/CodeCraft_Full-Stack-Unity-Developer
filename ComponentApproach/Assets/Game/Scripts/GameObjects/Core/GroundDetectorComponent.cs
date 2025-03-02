using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class GroundDetectorComponent
    {
        private readonly Collider2D _collider;
        private readonly LayerMask _layerMask;

        public GroundDetectorComponent(Collider2D collider, LayerMask layerMask)
        {
            _collider = collider;
            _layerMask = layerMask;
        }

        public bool IsGround() =>
            _collider.IsTouchingLayers(_layerMask);
    }
}