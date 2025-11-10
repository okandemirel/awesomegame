using UnityEngine;

namespace Modules.AudioModule.Scripts.Core.Data.ValueObjects
{
    [System.Serializable]
    public class SoundData
    {
        public SoundType type;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    public enum SoundType
    {
        CardFlip,
        Match,
        Mismatch,
        GameOver
    }
}