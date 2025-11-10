using UnityEngine;

namespace CardMatch.Modules.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "CardMatch/Audio Configuration")]
    public class AudioConfig : ScriptableObject
    {
        public AudioSettings settings;

        public SoundData GetSound(SoundType type)
        {
            return settings.sounds.Find(s => s.type == type);
        }
    }
}