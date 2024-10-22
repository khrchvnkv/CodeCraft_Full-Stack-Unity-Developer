using UnityEngine;

namespace Bullets
{
    [RequireComponent(
        typeof(BulletCollision), 
        typeof(BulletView),
        typeof(BulletVelocity))]
    public class BulletConstructor : MonoBehaviour
    {
        [SerializeField] private BulletCollision _collision;
        [SerializeField] private BulletView _view;
        [SerializeField] private BulletVelocity _velocity;

        private BulletManager _bulletManager;
        
        public Vector2 Position => transform.position;
        
        private void OnValidate()
        {
            _collision ??= gameObject.GetComponent<BulletCollision>();
            _view ??= gameObject.GetComponent<BulletView>();
            _velocity ??= gameObject.GetComponent<BulletVelocity>();
        }

        private void OnEnable() => _collision.Collided += Collided;
        
        private void OnDisable() => _collision.Collided -= Collided;

        private void Collided() => _bulletManager.DespawnBullet(this);

        public void Construct(
            in Vector2 position,
            in Color color,
            in int physicsLayer,
            in int damage,
            in Vector2 velocity,
            in BulletManager bulletManager)
        {
            transform.position = position;
            gameObject.layer = physicsLayer;

            _view.SetColor(color);
            _collision.SetDamage(damage);
            _velocity.SetVelocity(velocity);
            
            _bulletManager = bulletManager;
        }
    }
}