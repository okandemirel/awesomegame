using System.Collections.Generic;
using System.Linq;
using Modules.CardModule.Scripts.Core.Data;
using Modules.CardModule.Scripts.Core.Data.UnityObjects;
using Modules.GridModule.Scripts.Core.Data.UnityObjects;
using Modules.GridModule.Scripts.Core.Interfaces;
using UnityEngine;
using GridLayout = Modules.GridModule.Scripts.Core.Interfaces.GridLayout;

namespace Modules.GridModule.Scripts
{
    public class GridLayoutGenerator : IGridLayoutGenerator
    {
        public GridLayout GenerateLayout(GridConfigurationData config, Rect containerRect, CardConfig cardConfig)
        {
            var selectedCards = config.GetCardsForLevel(cardConfig);

            var layout = new GridLayout
            {
                Columns = config.settings.columns,
                Rows = config.settings.rows,
                CardSize = CalculateCardSize(config, containerRect),
                Positions = GeneratePositions(config, containerRect),
                CardTypes = GenerateShuffledPairs(selectedCards)
            };

            return layout;
        }

        public Vector2 CalculateCardSize(GridConfigurationData config, Rect containerRect)
        {
            float padding = containerRect.width * config.settings.paddingPercent;
            float availableWidth = containerRect.width - (padding * 2) - (config.settings.spacing.x * (config.settings.columns - 1));
            float availableHeight = containerRect.height - (padding * 2) - (config.settings.spacing.y * (config.settings.rows - 1));

            float cardWidth = availableWidth / config.settings.columns;
            float cardHeight = availableHeight / config.settings.rows;

            if (config.settings.maintainAspectRatio)
            {
                float size = Mathf.Min(cardWidth, cardHeight);
                return new Vector2(size, size) * config.settings.scaleMultiplier;
            }

            return new Vector2(cardWidth, cardHeight) * config.settings.scaleMultiplier;
        }

        private List<Vector3> GeneratePositions(GridConfigurationData config, Rect containerRect)
        {
            var positions = new List<Vector3>();
            Vector2 cardSize = CalculateCardSize(config, containerRect);
            float padding = containerRect.width * config.settings.paddingPercent;

            float totalWidth = (cardSize.x * config.settings.columns) + (config.settings.spacing.x * (config.settings.columns - 1));
            float totalHeight = (cardSize.y * config.settings.rows) + (config.settings.spacing.y * (config.settings.rows - 1));

            float startX = containerRect.xMin + padding + (containerRect.width - totalWidth - padding * 2) / 2;
            float startY = containerRect.yMax - padding - (containerRect.height - totalHeight - padding * 2) / 2;

            for (int row = 0; row < config.settings.rows; row++)
            {
                for (int col = 0; col < config.settings.columns; col++)
                {
                    float x = startX + (col * (cardSize.x + config.settings.spacing.x)) + cardSize.x / 2;
                    float y = startY - (row * (cardSize.y + config.settings.spacing.y)) - cardSize.y / 2;
                    positions.Add(new Vector3(x, y, 0));
                }
            }

            return positions;
        }

        private List<CardType> GenerateShuffledPairs(List<CardType> selectedCards)
        {
            var pairs = new List<CardType>();

            foreach (var cardType in selectedCards)
            {
                pairs.Add(cardType);
                pairs.Add(cardType);
            }

            return pairs.OrderBy(x => Random.value).ToList();
        }
    }
}