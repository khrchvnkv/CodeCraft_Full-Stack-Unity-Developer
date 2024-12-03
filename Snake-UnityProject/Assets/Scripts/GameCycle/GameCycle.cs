using Input;
using Modules;
using UI;
using Zenject;

namespace GameCycle
{
    public class GameCycle : IGameCycle, IInitializable
    {
        private readonly IInputAdapter _inputAdapter;
        private readonly IScreenManager _screenManager;
        private readonly IDifficulty _difficulty;

        public GameCycle(
            IInputAdapter inputAdapter, 
            IScreenManager screenManager, 
            IDifficulty difficulty)
        {
            _inputAdapter = inputAdapter;
            _screenManager = screenManager;
            _difficulty = difficulty;
        }

        void IInitializable.Initialize() => StartGame();

        public void StartGame()
        {
            _inputAdapter.Enable();
            _difficulty.Next(out _);
        }

        void IGameCycle.CompleteGame()
        {
            _inputAdapter.Disable();
            _screenManager.ShowGameCompleteScreen();
        }

        void IGameCycle.LossGame()
        {
            _inputAdapter.Disable();
            _screenManager.ShowGameLossScreen();
        }
    }
}