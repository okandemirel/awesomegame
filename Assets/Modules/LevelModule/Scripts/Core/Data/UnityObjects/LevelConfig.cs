using System.Collections.Generic;
using UnityEngine;

namespace Modules.LevelModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "CardMatch/Level Configuration")]
    public class LevelConfig : ScriptableObject
    {
        public List<LevelData> levels = new List<LevelData>();

        public List<LevelData> GetAutoLevels()
        {
            return levels.FindAll(l => l.levelType == LevelType.Auto);
        }

        public List<LevelData> GetCustomLevels()
        {
            return levels.FindAll(l => l.levelType == LevelType.Custom);
        }
    }
}
