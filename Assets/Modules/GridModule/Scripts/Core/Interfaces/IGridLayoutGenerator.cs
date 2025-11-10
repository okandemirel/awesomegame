using System.Collections.Generic;
using Modules.CardModule.Scripts.Core.Data;
using Modules.CardModule.Scripts.Core.Data.UnityObjects;
using Modules.GridModule.Scripts.Core.Data.UnityObjects;
using UnityEngine;

namespace Modules.GridModule.Scripts.Core.Interfaces
{
    public interface IGridLayoutGenerator
    {
        GridLayout GenerateLayout(GridConfigurationData config, Rect containerRect, CardConfig cardConfig);
        Vector2 CalculateCardSize(GridConfigurationData config, Rect containerRect);
    }

    public struct GridLayout
    {
        public int Columns;
        public int Rows;
        public Vector2 CardSize;
        public List<Vector3> Positions;
        public List<CardType> CardTypes;
    }
}