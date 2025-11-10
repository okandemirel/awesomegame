using UnityEngine;
using System.Collections.Generic;

namespace CardMatch.Modules.Card
{
    [System.Serializable]
    public class CardDatabaseSettings
    {
        public List<CardData> cards = new List<CardData>();
        public Sprite defaultBackSprite;
    }
}