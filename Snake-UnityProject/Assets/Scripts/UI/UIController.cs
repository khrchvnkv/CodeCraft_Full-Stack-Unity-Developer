using System;
using Modules;
using Zenject;

namespace UI
{
    public class UIController : IInitializable, IDisposable
    {
        private readonly IScreenManager _screenManager;
        private readonly IScore _score;
        private readonly IDifficulty _difficulty;

        public UIController(
            IScreenManager screenManager, 
            IScore score, 
            IDifficulty difficulty)
        {
            _screenManager = screenManager;
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

        private void UpdateScore(int score) => _screenManager.UpdateScore(score);

        private void UpdateDifficulty() => _screenManager.UpdateDifficulty(_difficulty.Current, _difficulty.Max);
    }
}