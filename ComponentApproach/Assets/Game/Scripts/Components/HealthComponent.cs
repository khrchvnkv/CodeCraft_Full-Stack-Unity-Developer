using System;

namespace Game.Scripts.Components
{
    public class HealthComponent
    {
        private readonly int _maxHp;
        private int _hp;

        private int Hp
        {
            get => _hp;
            set
            {
                if (_hp == value)
                {
                    return;
                }

                bool increased = value > _hp;
                
                _hp = Math.Max(0, value);
                HealthPointsChanged?.Invoke(_hp);
                if (increased)
                {
                    HealthPointsIncreased?.Invoke();
                }
                else
                {
                    HealthPointsDecreased?.Invoke();
                }
                
                if (_hp == 0)
                {
                    Die();
                }
            }
        }
        public bool IsAlive => _hp > 0;

        public event Action HealthPointsIncreased;
        public event Action HealthPointsDecreased;
        public event Action<int> HealthPointsChanged;
        public event Action Died;

        public HealthComponent(int maxHp)
        {
            _maxHp = maxHp;
            Hp = _maxHp;
        }
        
        public void TakeDamage(in int damage)
        {
            Hp = Math.Max(0, Hp - damage);
        }

        public void Kill() => Hp = 0;

        private void Die() => Died?.Invoke();
    }
}