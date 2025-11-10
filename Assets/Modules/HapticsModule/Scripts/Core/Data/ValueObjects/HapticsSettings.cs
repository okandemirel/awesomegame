using UnityEngine;

namespace Modules.HapticsModule.Scripts.Core.Data.ValueObjects
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