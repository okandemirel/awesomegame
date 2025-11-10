using Modules.SaveModule.Scripts.Core.Data.ValueObjects;

namespace Modules.SaveModule.Scripts.Core.Interfaces
{
    public interface IGameSaveManager
    {
        void Save(SaveData data);
        SaveData Load();
        bool HasSaveData();
        void DeleteSave();
    }
}