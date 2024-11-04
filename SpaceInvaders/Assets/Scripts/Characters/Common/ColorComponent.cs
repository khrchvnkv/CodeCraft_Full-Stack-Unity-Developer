using System;
using UnityEngine;

namespace Characters.Common
{
    [Serializable]
    public sealed class ColorComponent 
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void SetColor(in Color newColor) => _spriteRenderer.color = newColor;
    }
}