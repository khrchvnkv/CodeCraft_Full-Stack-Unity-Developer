using System;
using UnityEngine;

namespace Game.Scripts.GameObjects.Content.Triggers
{
    public sealed class TriggerEventReceiver : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggerEnter; 
        public event Action<Collider2D> OnTriggerExit; 

        private void OnTriggerEnter2D(Collider2D other) => OnTriggerEnter?.Invoke(other);
        private void OnTriggerExit2D(Collider2D other) => OnTriggerExit?.Invoke(other);
    }
}