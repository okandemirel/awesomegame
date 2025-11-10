using System.Collections.Generic;

namespace Modules.SaveModule.Scripts.Core.Data.ValueObjects
{
    [System.Serializable]
    public class SaveData
    {
        public int score;
        public int moves;
        public int matchesFound;
        public float elapsedTime;
        public List<CardSaveData> cards;
        public string gridConfigName;
    }

    [System.Serializable]
    public class CardSaveData
    {
        public int id;
        public int cardTypeId;
        public bool isFlipped;
        public bool isMatched;
    }
}