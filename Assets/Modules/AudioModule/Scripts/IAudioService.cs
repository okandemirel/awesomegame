using Modules.AudioModule.Scripts.Core.Data.ValueObjects;

namespace Modules.AudioModule.Scripts
{
    public interface IAudioService
    {
        void Initialize();
        void PlaySound(SoundType type);
        void SetMasterVolume(float volume);
        void Cleanup();
    }
}