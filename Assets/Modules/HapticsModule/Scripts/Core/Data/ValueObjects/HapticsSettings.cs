using UnityEngine;

namespace CardMatch.Modules.Haptics
{
    [System.Serializable]
    public class HapticsSettings
    {
        public bool enableHaptics = true;
        [Range(0f, 1f)]
        public float intensity = 1f;
        public bool enableOnMobile = true;
        public bool enableOnDesktop = false;
    }
}