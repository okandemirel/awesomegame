using Modules.HapticsModule.Scripts.Core.Data.UnityObjects;
using UnityEngine;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace Modules.HapticsModule.Scripts
{
    public class HapticsEngine : IHapticsEngine
    {
        private HapticsConfig _config;

        public bool IsSupported
        {
            get
            {
#if UNITY_IOS || UNITY_ANDROID
                return SystemInfo.supportsVibration;
#else
                return false;
#endif
            }
        }

        public HapticsEngine(HapticsConfig config)
        {
            _config = config;
        }

        public void TriggerLight()
        {
            if (ShouldTrigger())
            {
#if UNITY_IOS || UNITY_ANDROID
                Handheld.Vibrate();
#endif
            }
        }

        public void TriggerMedium()
        {
            if (ShouldTrigger())
            {
#if UNITY_IOS || UNITY_ANDROID
                Handheld.Vibrate();
#endif
            }
        }

        public void TriggerHeavy()
        {
            if (ShouldTrigger())
            {
#if UNITY_IOS || UNITY_ANDROID
                Handheld.Vibrate();
#endif
            }
        }

        private bool ShouldTrigger()
        {
            if (!_config.settings.enableHaptics) return false;
            if (!IsSupported) return false;

#if UNITY_IOS || UNITY_ANDROID
            return _config.settings.enableOnMobile;
#else
            return _config.settings.enableOnDesktop;
#endif
        }

    }
}