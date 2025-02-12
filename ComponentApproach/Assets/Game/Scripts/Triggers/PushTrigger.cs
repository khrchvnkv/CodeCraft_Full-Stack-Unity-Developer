using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class PushTrigger : MonoBehaviour
    {
        public IEnumerable<Rigidbody2D> Targets => _targets;

        private readonly HashSet<Rigidbody2D> _targets = new();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Add(rb);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Rigidbody2D rb))
            {
                _targets.Remove(rb);
            }
        }
    }
}