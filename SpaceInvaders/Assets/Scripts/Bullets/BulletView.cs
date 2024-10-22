using UnityEngine;

namespace Bullets
{
    public sealed class BulletView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetColor(in Color newColor) => _spriteRenderer.color = newColor;
    }
}