using Input;
using Modules;
using SnakeGame;
using Zenject;

namespace GameCycle
{
    public class GameCycle : IGameCycle, IInitializable
    {
        private readonly IInputAdapter _inputAdapter;
        private readonly IGameUI _gameUI;
        private readonly IDifficulty _difficulty;

        public GameCycle(
            IInputAdapter inputAdapter, 
            IGameUI gameUI, 
            IDifficulty difficulty)
        {
            _inputAdapter = inputAdapter;
            _gameUI = gameUI;
            _difficulty = difficulty;
        }

        void IInitializable.Initialize() => StartGame();

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