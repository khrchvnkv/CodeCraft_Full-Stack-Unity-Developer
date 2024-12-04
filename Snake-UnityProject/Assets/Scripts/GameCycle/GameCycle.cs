using System;
using Coin;
using Input;
using Modules;
using SnakeGame;
using Zenject;

namespace GameCycle
{
    public class GameCycle : IGameCycle, IInitializable, IDisposable
    {
        private readonly IInputAdapter _inputAdapter;
        private readonly IGameUI _gameUI;
        private readonly IDifficulty _difficulty;
        private readonly ICoinManager _coinManager;

        public GameCycle(
            IInputAdapter inputAdapter, 
            IGameUI gameUI, 
            IDifficulty difficulty,
            ICoinManager coinManager)
        {
            _inputAdapter = inputAdapter;
            _gameUI = gameUI;
            _difficulty = difficulty;
            _coinManager = coinManager;
        }

        void IInitializable.Initialize()
        {
            StartGame();

            _coinManager.OnAllCoinsCollected += StartNewStageOrCompleteGame;
        }

        void IDisposable.Dispose()
        {
            _coinManager.OnAllCoinsCollected -= StartNewStageOrCompleteGame;
        }

        private void StartNewStageOrCompleteGame()
        {
            if (!_difficulty.Next(out _))
            {
                CompleteGame();
            }
        }

        public void StartGame()
        {
            _inputAdapter.Enable();
            _difficulty.Next(out _);
        }

        public void CompleteGame()
        {
            _inputAdapter.Disable();
            _gameUI.GameOver(true);
        }

        void IGameCycle.LossGame()
        {
            _inputAdapter.Disable();
            _gameUI.GameOver(false);
        }
    }
}