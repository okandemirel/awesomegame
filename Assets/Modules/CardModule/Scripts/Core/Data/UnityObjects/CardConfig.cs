using System.Collections.Generic;
using System.Linq;
using Modules.CardModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;

namespace Modules.CardModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "CardMatch/Card Configuration")]
    public class CardConfig : ScriptableObject
    {
        public CardDatabaseSettings settings;

        public CardData GetCardByType(CardType type)
        {
            return settings.cards.FirstOrDefault(c => c.cardType == type);
        }

        public CardData GetCardById(int id)
        {
            return settings.cards.FirstOrDefault(c => c.GetId() == id);
        }

        public List<CardType> GetAvailableCards()
        {
            return settings.cards.Select(c => c.cardType).ToList();
        }

        public Sprite GetDefaultBackSprite() => settings.defaultBackSprite;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (settings.cards == null) return;

            var duplicates = settings.cards.GroupBy(c => c.cardType)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);

            if (duplicates.Any())
            {
                Debug.LogWarning($"Duplicate card types found in {name}: {string.Join(", ", duplicates)}");
            }
        }
#endif
    }
}