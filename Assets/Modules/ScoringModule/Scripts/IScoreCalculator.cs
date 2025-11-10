namespace Modules.ScoringModule.Scripts
{
    public struct ScoringContext
    {
        public bool IsMatch;
        public float TimeTaken;
        public int CurrentCombo;
    }

    public interface IScoreCalculator
    {
        void Initialize();
        int CalculateScore(ScoringContext context);
        void AddCombo();
        void ResetCombo();
        int CurrentCombo { get; }
    }
}