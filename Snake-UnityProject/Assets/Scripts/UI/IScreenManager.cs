namespace UI
{
    public interface IScreenManager
    {
        void ShowGameCompleteScreen();
        void ShowGameLossScreen();
        void UpdateScore(in int score);
        void UpdateDifficulty(in int current, in int max);
    }
}