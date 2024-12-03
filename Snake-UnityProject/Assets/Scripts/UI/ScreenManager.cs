using SnakeGame;

namespace UI
{
    public class ScreenManager : IScreenManager
    {
        private readonly IGameUI _gameUI;

        public ScreenManager(IGameUI gameUI)
        {
            _gameUI = gameUI;
        }

        void IScreenManager.ShowGameCompleteScreen() => _gameUI.GameOver(true);

        void IScreenManager.ShowGameLossScreen() => _gameUI.GameOver(false);

        void IScreenManager.UpdateScore(in int score) => _gameUI.SetScore(score.ToString());

        void IScreenManager.UpdateDifficulty(in int current, in int max) => _gameUI.SetDifficulty(current, max);
    }
}