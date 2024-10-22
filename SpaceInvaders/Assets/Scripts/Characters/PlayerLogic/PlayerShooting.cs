using Bullets;
using Common;
using UnityEngine;

namespace Characters.PlayerLogic
{
    public sealed class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletManager _bulletManager;

        public void Shoot()
        {
            _bulletManager.SpawnBullet(
                _firePoint.position,
                Color.blue,
                (int) PhysicsLayer.PLAYER_BULLET,
                1,
                _firePoint.rotation * Vector3.up * 3
            );
        }
    }
}