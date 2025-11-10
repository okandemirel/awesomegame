namespace CardMatch.Modules.Haptics
{
    public interface IHapticsEngine
    {
        void TriggerLight();
        void TriggerMedium();
        void TriggerHeavy();
        bool IsSupported { get; }
    }
}