using Bullets.Factory;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletCollision _collision;
        [SerializeField] private BulletView _view;
        [SerializeField] private BulletVelocity _velocity;

        private IBulletDestroyCallback _destroyCallback;
        
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
            _collision.SetDamage(damage);
            _velocity.SetVelocity(velocity);

            _destroyCallback = destroyCallback;
        }
        
        private void OnEnable() => _collision.Collided += Collided;
        
        private void OnDisable() => _collision.Collided -= Collided;

        private void Collided() => _destroyCallback?.Destroy(this);

        private void OnCollisionEnter2D(Collision2D other) => 
            _collision.OnCollisionEnter2D(other);
    }
}