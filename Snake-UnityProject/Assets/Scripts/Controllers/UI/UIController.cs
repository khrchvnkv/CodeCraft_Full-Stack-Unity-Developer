using System;
using Modules;
using SnakeGame;
using Zenject;

namespace Controllers.UI
{
    public class UIController : IUIController, IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;
        private readonly IGameUI _gameUI;

        public UIController(IScore score, IDifficulty difficulty, IGameUI gameUI)
        {
            _score = score;
            _difficulty = difficulty;
            _gameUI = gameUI;
        }

        void IInitializable.Initialize()
        {
            _score.OnStateChanged += UpdateScore;
            _difficulty.OnStateChanged += UpdateDifficulty;
            
            UpdateScore(_score.Current);
            UpdateDifficulty();
        }

        void IDisposable.Dispose()
        {
            _score.OnStateChanged -= UpdateScore;
            _difficulty.OnStateChanged -= UpdateDifficulty;
        }

        private void UpdateScore(int score) => _gameUI.SetScore(score.ToString());

        private void UpdateDifficulty() => _gameUI.SetDifficulty(_difficulty.Current, _difficulty.Max);
    }
}