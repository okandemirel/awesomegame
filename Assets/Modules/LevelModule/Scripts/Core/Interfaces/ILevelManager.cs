using System;
using Modules.LevelModule.Scripts.Core.Data;

namespace Modules.LevelModule.Scripts.Core.Interfaces
{
    public interface ILevelManager
    {
        event Action<LevelData> OnLevelSelected;
        void SelectLevel(LevelData level);
        LevelData GetCurrentLevel();
    }
}
