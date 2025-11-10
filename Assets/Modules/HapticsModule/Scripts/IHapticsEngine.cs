namespace Modules.HapticsModule.Scripts
{
    public interface IHapticsEngine
    {
        void TriggerLight();
        void TriggerMedium();
        void TriggerHeavy();
        bool IsSupported { get; }
    }
}