using UnityEngine;

namespace Modules.CardModule.Scripts.Core.Data
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