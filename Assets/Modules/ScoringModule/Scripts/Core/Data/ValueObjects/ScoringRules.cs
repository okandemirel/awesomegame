using UnityEngine;

namespace Modules.ScoringModule.Scripts.Core.Data.ValueObjects
{
    [System.Serializable]
    public class ScoringRules
    {
        [Header("Base Points")]
        public int matchBasePoints = 100;
        public int perfectMatchBonus = 50;

        [Header("Combo System")]
        public bool enableCombo = true;
        public int comboMultiplierIncrement = 1;
        public float comboResetTime = 3f;

        [Header("Penalties")]
        public int mismatchPenalty = -10;
        public bool allowNegativeScore = false;
    }
}