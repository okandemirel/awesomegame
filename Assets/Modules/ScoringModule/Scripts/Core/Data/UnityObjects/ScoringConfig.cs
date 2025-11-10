using Modules.ScoringModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;

namespace Modules.ScoringModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "ScoringConfig", menuName = "CardMatch/Scoring Configuration")]
    public class ScoringConfig : ScriptableObject
    {
        public ScoringRules rules;
    }
}