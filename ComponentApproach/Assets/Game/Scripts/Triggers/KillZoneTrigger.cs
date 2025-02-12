using Game.Scripts.Contracts;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class KillZoneTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDieable killable))
            {
                killable.Die();
            }
        }
    }
}