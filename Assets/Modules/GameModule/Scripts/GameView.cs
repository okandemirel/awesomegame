using Modules.CardModule.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameModule.Scripts
{
    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private Transform cardContainer;
        [SerializeField] private RectTransform containerRect;
        [SerializeField] private CardPool cardPool;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI movesText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TextMeshProUGUI gameOverScoreText;

        private void Awake()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }

        public void ShowScore(int score, int combo)
        {
            if (scoreText != null)
            {
                string comboText = combo > 0 ? $" (x{combo + 1})" : "";
                scoreText.text = $"Score: {score}{comboText}";
            }
        }

        public void ShowMoves(int moves)
        {
            if (movesText != null)
            {
                movesText.text = $"Moves: {moves}";
            }
        }

        public void ShowGameOver(int finalScore)
        {
            ShowGameComplete(finalScore);
        }

        public void ShowGameComplete(int finalScore)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            if (gameOverScoreText != null)
            {
                gameOverScoreText.text = $"Final Score: {finalScore}";
            }
        }

        public Transform GetCardContainer()
        {
            return cardContainer;
        }

        public Rect GetContainerRect()
        {
            if (containerRect != null)
            {
                return containerRect.rect;
            }
            return new Rect(0, 0, Screen.width, Screen.height);
        }

        public CardPool GetCardPool()
        {
            return cardPool;
        }

        public void ConfigureGridLayout(int columns, Vector2 cellSize, Vector2 spacing)
        {
            var gridLayout = cardContainer.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                gridLayout.constraintCount = columns;
                gridLayout.cellSize = cellSize;
                gridLayout.spacing = spacing;
            }
        }
    }
}