using Bullets.Factory;
using Characters.Common;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ColorComponent _view;
        [SerializeField] private VelocityComponent _velocity;

        private IBulletDestroyCallback _destroyCallback;
        private int _damage;
        
        public Vector2 Position => transform.position;
        
        public void Construct(
            in Vector2 position,
            in Color color,
            in int physicsLayer,
            in int damage,
            in Vector2 velocity,
            IBulletDestroyCallback destroyCallback)
        {
            transform.position = position;
            gameObject.layer = physicsLayer;

            _view.SetColor(color);
            SetDamage(damage);
            _velocity.SetVelocity(velocity);

            _destroyCallback = destroyCallback;
        }
        
        private void SetDamage(in int damage) => _damage = damage;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                OnBulletCollided(damageable);
            }
        }

        private void OnBulletCollided(in IDamageable damageable)
        {
            damageable.DealDamage(_damage);
            _destroyCallback?.Destroy(this);
        }
    }
}