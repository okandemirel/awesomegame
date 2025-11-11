using System;
using System.Collections.Generic;
using Modules.LevelModule.Scripts.Core.Data;
using Modules.LevelModule.Scripts.Core.Data.UnityObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.LevelModule.Scripts.View
{
    public class LevelSelectionView : MonoBehaviour
    {
        [SerializeField] private GameObject selectionPanel;
        [SerializeField] private Transform autoLevelsContainer;
        [SerializeField] private Transform customLevelsContainer;
        [SerializeField] private Button levelButtonPrefab;
        [SerializeField] private Button backButton;

        public event Action<LevelData> OnLevelSelected;
        public event Action OnBackClicked;

        private List<Button> _levelButtons = new List<Button>();

        private void Awake()
        {
            backButton.onClick.AddListener(HandleBackClick);
        }

        public void Initialize(LevelConfig config)
        {
            ClearButtons();

            foreach (var level in config.GetAutoLevels())
            {
                CreateLevelButton(level, autoLevelsContainer);
            }

            foreach (var level in config.GetCustomLevels())
            {
                CreateLevelButton(level, customLevelsContainer);
            }
        }

        private void CreateLevelButton(LevelData level, Transform parent)
        {
            var button = Instantiate(levelButtonPrefab, parent);
            button.GetComponentInChildren<TextMeshProUGUI>().text = level.GetDisplayName();
            button.onClick.AddListener(() => HandleLevelClick(level));
            _levelButtons.Add(button);
            button.gameObject.SetActive(true);
        }

        private void HandleLevelClick(LevelData level)
        {
            OnLevelSelected?.Invoke(level);
        }

        private void HandleBackClick()
        {
            OnBackClicked?.Invoke();
        }

        private void ClearButtons()
        {
            foreach (var button in _levelButtons)
            {
                if (button != null)
                {
                    button.onClick.RemoveAllListeners();
                    Destroy(button.gameObject);
                }
            }
            _levelButtons.Clear();
        }

        public void Show()
        {
            selectionPanel.SetActive(true);
        }

        public void Hide()
        {
            selectionPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveListener(HandleBackClick);
            ClearButtons();
        }
    }
}
