using UnityEngine;

namespace Characters.PlayerLogic
{
    public class PlayerDeathObserver : MonoBehaviour
    {
        [SerializeField] private Ship _ship;

        private void OnEnable() => _ship.OnDied += StopSimulation;

        private void OnDisable() => _ship.OnDied -= StopSimulation;

        private void StopSimulation() => Time.timeScale = 0;
    }
}