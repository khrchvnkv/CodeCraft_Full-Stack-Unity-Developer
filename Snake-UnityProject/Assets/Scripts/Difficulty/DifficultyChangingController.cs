using System;
using Coin;
using GameCycle;
using Modules;
using Zenject;

namespace Difficulty
{
    public class DifficultyChangingController : IInitializable, IDisposable
    {
        private readonly ICoinManager _coinManager;
        private readonly IGameCycle _gameCycle;
        private readonly IDifficulty _difficulty;

        public DifficultyChangingController(
            ICoinManager coinManager, 
            IGameCycle gameCycle,
            IDifficulty difficulty)
        {
            _coinManager = coinManager;
            _gameCycle = gameCycle;
            _difficulty = difficulty;
        }

        public void Initialize() => 
            _coinManager.OnAllCoinsCollected += StartNewStageOrCompleteGame;

        public void Dispose() => 
            _coinManager.OnAllCoinsCollected += StartNewStageOrCompleteGame;

        private void StartNewStageOrCompleteGame()
        {
            if (!_difficulty.Next(out _))
            {
                _gameCycle.CompleteGame();
            }
        }
    }
}