using System;
using Modules.LevelModule.Scripts.Core.Data;
using Modules.LevelModule.Scripts.Core.Interfaces;

namespace Modules.LevelModule.Scripts
{
    public class LevelManager : ILevelManager
    {
        private LevelData _currentLevel;

        public event Action<LevelData> OnLevelSelected;

        public void SelectLevel(LevelData level)
        {
            _currentLevel = level;
            OnLevelSelected?.Invoke(level);
        }

        public LevelData GetCurrentLevel()
        {
            return _currentLevel;
        }
    }
}
