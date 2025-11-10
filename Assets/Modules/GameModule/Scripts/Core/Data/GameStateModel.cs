namespace Modules.GameModule.Scripts.Core.Data
{
    public enum GameState
    {
        Idle,
        Playing,
        Comparing,
        GameOver
    }

    public class GameStateModel
    {
        public int Score { get; set; }
        public int Moves { get; set; }
        public int MatchesFound { get; set; }
        public float ElapsedTime { get; set; }
        public GameState CurrentState { get; set; }
        public int TotalPairs { get; set; }

        public bool IsGameComplete => MatchesFound >= TotalPairs;
    }
}