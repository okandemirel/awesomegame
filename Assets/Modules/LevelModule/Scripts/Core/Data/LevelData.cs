using Modules.GridModule.Scripts.Core.Data.UnityObjects;
using UnityEngine;

namespace Modules.LevelModule.Scripts.Core.Data
{
    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public LevelType levelType;
        public GridConfigurationData gridConfig;
        public Sprite thumbnail;

        public string GetDisplayName()
        {
            if (levelType == LevelType.Auto && gridConfig != null)
            {
                return $"{gridConfig.settings.columns}x{gridConfig.settings.rows}";
            }
            return levelName;
        }
    }
}
