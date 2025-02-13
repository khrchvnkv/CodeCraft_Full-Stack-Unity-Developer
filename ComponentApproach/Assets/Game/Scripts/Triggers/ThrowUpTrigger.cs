using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Triggers
{
    public class ThrowUpTrigger : MonoBehaviour
    {
        private ThrowUpComponent _throwUpComponent;

        [Inject]
        private void Construct(ThrowUpComponent throwUpComponent)
        {
            _throwUpComponent = throwUpComponent;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Rigidbody2D rb))
            {
                _throwUpComponent.ThrowUp(rb);
            }
        }
    }
}