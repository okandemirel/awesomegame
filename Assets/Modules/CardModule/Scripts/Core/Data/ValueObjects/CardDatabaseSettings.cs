using System.Collections.Generic;
using UnityEngine;

namespace Modules.CardModule.Scripts.Core.Data.ValueObjects
{
    [System.Serializable]
    public class CardDatabaseSettings
    {
        public List<CardData> cards = new List<CardData>();
        public Sprite defaultBackSprite;
    }
}