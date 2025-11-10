using System.Collections.Generic;
using Modules.CardModule.Scripts.Core.Data;
using UnityEngine;

namespace Modules.GridModule.Scripts.Core.Data.ValueObjects
{
    [System.Serializable]
    public class GridSettings
    {
        [Header("Grid Dimensions")]
        public int columns = 4;
        public int rows = 4;

        [Header("Card Selection")]
        public List<CardType> cardsToUse = new List<CardType>();
        public bool autoSelectCards = true;

        [Header("Layout Settings")]
        public Vector2 spacing = new Vector2(0.1f, 0.1f);
        public float paddingPercent = 0.05f;

        [Header("Scaling")]
        public bool maintainAspectRatio = true;
        [Range(0.5f, 2f)]
        public float scaleMultiplier = 1f;

        [Header("Animation")]
        public float cardSpawnDelay = 0.05f;
        public AnimationCurve spawnCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}