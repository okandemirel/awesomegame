using Modules.CardModule.Scripts;
using UnityEngine;

namespace Modules.GameModule.Scripts
{
    public interface IGameView
    {
        void ShowScore(int score, int combo);
        void ShowMoves(int moves);
        void ShowGameOver(int finalScore);
        void ShowGameComplete(int finalScore);
        Transform GetCardContainer();
        Rect GetContainerRect();
        CardPool GetCardPool();
        void ConfigureGridLayout(int columns, Vector2 cellSize, Vector2 spacing);
    }
}