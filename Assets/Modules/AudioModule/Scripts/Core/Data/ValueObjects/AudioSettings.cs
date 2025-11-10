using UnityEngine;
using System.Collections.Generic;

namespace CardMatch.Modules.Audio
{
    [System.Serializable]
    public class AudioSettings
    {
        public List<SoundData> sounds = new List<SoundData>();

        [Range(0f, 1f)]
        public float masterVolume = 1f;

        public bool useObjectPooling = true;
        public int poolSize = 5;
    }
}