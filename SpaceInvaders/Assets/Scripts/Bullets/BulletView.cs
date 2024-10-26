using System;
using UnityEngine;

namespace Bullets
{
    [Serializable]
    public sealed class BulletView
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetColor(in Color newColor) => _spriteRenderer.color = newColor;
    }
}