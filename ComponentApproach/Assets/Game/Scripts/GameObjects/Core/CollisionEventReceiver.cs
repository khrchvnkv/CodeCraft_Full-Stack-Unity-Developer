using System;
using Modules.Entity;
using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class CollisionEventReceiver : MonoBehaviour
    {
        public event Action<Entity> OnCollisionEnter; 
        public event Action<Entity> OnCollisionExit; 

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
            {
                OnCollisionEnter?.Invoke(entity);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Entity entity))
            {
                OnCollisionExit?.Invoke(entity);
            }
        }
    }
}