using UnityEngine;

namespace CardMatch.Modules.Haptics
{
    [CreateAssetMenu(fileName = "HapticsConfig", menuName = "CardMatch/Haptics Configuration")]
    public class HapticsConfig : ScriptableObject
    {
        public HapticsSettings settings;
    }
}