using Bullets.Factory;
using Characters;
using UnityEngine;

namespace Level
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Ship _player;
        [SerializeField] private BulletFactory _bulletFactory;

        private void Awake() => _player.Construct(_bulletFactory);
    }
}