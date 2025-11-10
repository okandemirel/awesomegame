using Modules.HapticsModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;

namespace Modules.HapticsModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "HapticsConfig", menuName = "CardMatch/Haptics Configuration")]
    public class HapticsConfig : ScriptableObject
    {
        public HapticsSettings settings;
    }
}