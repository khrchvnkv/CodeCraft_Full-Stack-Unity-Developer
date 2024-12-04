using System;
using Modules;
using SnakeGame;
using Zenject;

namespace UI
{
    public class UIController : IInitializable, IDisposable
    {
        private readonly IGameUI _gameUI;
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;

        public UIController(
            IGameUI gameUI, 
            IScore score, 
            IDifficulty difficulty)
        {
            _gameUI = gameUI;
            _score = score;
            _difficulty = difficulty;
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