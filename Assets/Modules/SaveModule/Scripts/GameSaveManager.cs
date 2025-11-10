using Modules.SaveModule.Scripts.Core.Data.UnityObjects;
using Modules.SaveModule.Scripts.Core.Data.ValueObjects;
using Modules.SaveModule.Scripts.Core.Interfaces;
using UnityEngine;

namespace Modules.SaveModule.Scripts
{
    public class GameSaveManager : IGameSaveManager
    {
        private SaveConfig _config;

        public GameSaveManager(SaveConfig config)
        {
            _config = config;
        }

        public void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_config.settings.saveKey, json);
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (!HasSaveData()) return null;

            string json = PlayerPrefs.GetString(_config.settings.saveKey);
            return JsonUtility.FromJson<SaveData>(json);
        }

        public bool HasSaveData()
        {
            return PlayerPrefs.HasKey(_config.settings.saveKey);
        }

        public void DeleteSave()
        {
            PlayerPrefs.DeleteKey(_config.settings.saveKey);
            PlayerPrefs.Save();
        }

    }
}