namespace CardMatch.Modules.Audio
{
    public interface IAudioService
    {
        void Initialize();
        void PlaySound(SoundType type);
        void SetMasterVolume(float volume);
        void Cleanup();
    }
}