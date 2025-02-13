using Game.Scripts.Contracts;
using UnityEngine;

namespace Game.Scripts.Triggers
{
    public class KillZoneTrigger : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDieable dieable))
            {
                dieable.Die();
                _audioSource.Play();
            }
        }
    }
}