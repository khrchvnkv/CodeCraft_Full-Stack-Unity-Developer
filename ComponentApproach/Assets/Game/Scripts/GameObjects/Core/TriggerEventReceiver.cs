using System;
using Modules.Entity;
using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public sealed class TriggerEventReceiver : MonoBehaviour
    {
        public event Action<Entity> OnTriggerEnter; 
        public event Action<Entity> OnTriggerExit; 

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                OnTriggerEnter?.Invoke(entity);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Entity entity))
            {
                OnTriggerExit?.Invoke(entity);
            }
        }
    }
}