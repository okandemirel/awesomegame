using System.Collections.Generic;
using Modules.CardModule.Scripts.Core.Data;
using Modules.CardModule.Scripts.Core.Data.UnityObjects;
using Modules.GridModule.Scripts.Core.Data.ValueObjects;
using UnityEngine;

namespace Modules.GridModule.Scripts.Core.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "CardMatch/Grid Configuration")]
    public class GridConfigurationData : ScriptableObject
    {
        public GridSettings settings;

        public int TotalCards => settings.columns * settings.rows;
        public int UniquePairs => TotalCards / 2;

        public List<CardType> GetCardsForLevel(CardConfig cardConfig)
        {
            if (settings.autoSelectCards || settings.cardsToUse.Count == 0)
            {
                var availableCards = cardConfig.GetAvailableCards();
                var selected = new List<CardType>();

                for (int i = 0; i < UniquePairs && i < availableCards.Count; i++)
                {
                    selected.Add(availableCards[i]);
                }

                return selected;
            }

            return settings.cardsToUse.GetRange(0, Mathf.Min(settings.cardsToUse.Count, UniquePairs));
        }
    }
}