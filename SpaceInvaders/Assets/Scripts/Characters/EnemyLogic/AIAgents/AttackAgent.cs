using System;
using Characters.Common;
using UnityEngine;

namespace Characters.EnemyLogic.AIAgents
{
    [Serializable]
    public class AttackAgent
    {
        [SerializeField] private float _countdown;
        
        private Ship _ship;
        private Ship _target;
        private float _currentTime;

        private bool IsTimerExpired => _currentTime <= 0;

        public void Construct(
            in Ship ship,
            in Ship target)
        {
            _ship = ship;
            _target = target;
            ResetTimer();
        }

        public void Shoot()
        {
            if (!_target.IsAlive)
                return;

            _currentTime -= Time.fixedDeltaTime;
            if (IsTimerExpired)
            {
                var direction = _target.Position - _ship.Position;
                _ship.Shoot(direction);
                ResetTimer();
            }
        }

        private void ResetTimer() => _currentTime = _countdown;
    }
}