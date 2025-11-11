using Modules.CardModule.Scripts;
using Modules.CardModule.Scripts.View;
using Modules.GameModule.Scripts;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class LevelDesignerWindow : EditorWindow
{
    private int _columns = 4;
    private int _rows = 4;
    private Vector2 _cellSize = new Vector2(180, 180);
    private Vector2 _spacing = new Vector2(10, 10);
    private GameObject _cardPrefab;

    [MenuItem("CardMatch/Level Designer")]
    public static void ShowWindow()
    {
        GetWindow<LevelDesignerWindow>("Level Designer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Configuration", EditorStyles.boldLabel);

        _columns = EditorGUILayout.IntField("Columns", _columns);
        _rows = EditorGUILayout.IntField("Rows", _rows);
        _cellSize = EditorGUILayout.Vector2Field("Cell Size", _cellSize);
        _spacing = EditorGUILayout.Vector2Field("Spacing", _spacing);

        EditorGUILayout.Space();
        _cardPrefab = EditorGUILayout.ObjectField("Card Prefab", _cardPrefab, typeof(GameObject), false) as GameObject;

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Grid in Scene", GUILayout.Height(30)))
        {
            GenerateGridInScene();
        }

        if (GUILayout.Button("Update Existing Grid", GUILayout.Height(30)))
        {
            UpdateExistingGrid();
        }
    }

    private void GenerateGridInScene()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null)
        {
            EditorUtility.DisplayDialog("Error", "No Canvas found in scene. Use CardMatch > Setup Scene first.", "OK");
            return;
        }

        var gameView = canvas.GetComponentInChildren<GameView>();
        if (gameView == null)
        {
            EditorUtility.DisplayDialog("Error", "No GameView found. Use CardMatch > Setup Scene first.", "OK");
            return;
        }

        var cardContainer = gameView.transform.Find("CardContainer");
        if (cardContainer == null)
        {
            EditorUtility.DisplayDialog("Error", "No CardContainer found.", "OK");
            return;
        }

        var gridLayout = cardContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = _columns;
            gridLayout.cellSize = _cellSize;
            gridLayout.spacing = _spacing;
            EditorUtility.SetDirty(gridLayout);
        }

        var cardPool = cardContainer.GetComponent<CardPool>();
        if (cardPool == null)
        {
            cardPool = cardContainer.gameObject.AddComponent<CardPool>();
        }

        int totalCards = _columns * _rows;
        int currentCardCount = cardContainer.childCount;

        if (_cardPrefab == null)
        {
            _cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Card.prefab");
        }

        if (_cardPrefab == null)
        {
            EditorUtility.DisplayDialog("Error", "Card prefab not found at Assets/Prefabs/Card.prefab", "OK");
            return;
        }

        if (currentCardCount < totalCards)
        {
            for (int i = currentCardCount; i < totalCards; i++)
            {
                var cardInstance = PrefabUtility.InstantiatePrefab(_cardPrefab, cardContainer) as GameObject;
                cardInstance.name = $"Card_{i}";
                cardInstance.SetActive(false);

                var cardView = cardInstance.GetComponent<CardView>();
                if (cardView != null)
                {
                    cardPool.RegisterCard(cardView);
                }
            }
        }

        EditorUtility.SetDirty(cardContainer.gameObject);
    }

    private void UpdateExistingGrid()
    {
        var canvas = GameObject.Find("Canvas");
        if (canvas == null) return;

        var gameView = canvas.GetComponentInChildren<GameView>();
        if (gameView == null) return;

        var cardContainer = gameView.transform.Find("CardContainer");
        if (cardContainer == null) return;

        var gridLayout = cardContainer.GetComponent<GridLayoutGroup>();
        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = _columns;
            gridLayout.cellSize = _cellSize;
            gridLayout.spacing = _spacing;
            EditorUtility.SetDirty(gridLayout);
        }
    }
}