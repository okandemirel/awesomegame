using UnityEngine;

namespace CardMatch.Modules.Card
{
    [System.Serializable]
    public class CardData
    {
        public CardType cardType;
        public Sprite frontSprite;
        public Color cardColor = Color.white;

        public int GetId() => (int)cardType;
    }
}