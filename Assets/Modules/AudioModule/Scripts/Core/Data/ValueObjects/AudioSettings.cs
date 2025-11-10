using System.Collections.Generic;
using UnityEngine;

namespace Modules.AudioModule.Scripts.Core.Data.ValueObjects
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