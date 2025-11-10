using Modules.AudioModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;
using AudioSettings = Modules.AudioModule.Scripts.Core.Data.ValueObjects.AudioSettings;

namespace Modules.AudioModule.Scripts.Core.Data.UnityObjects
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