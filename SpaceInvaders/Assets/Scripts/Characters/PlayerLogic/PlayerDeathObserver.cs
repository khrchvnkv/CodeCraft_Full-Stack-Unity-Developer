using UnityEngine;

namespace Characters.PlayerLogic
{
    public class PlayerDeathObserver : DeathObserver
    {
        [SerializeField] private Ship _ship;

        protected override void Died() => StopSimulation();
        private void StopSimulation() => Time.timeScale = 0;
    }
}