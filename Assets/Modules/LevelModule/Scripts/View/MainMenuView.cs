using System;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.LevelModule.Scripts.View
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject menuPanel;

        public event Action OnPlayClicked;

        private void Awake()
        {
            playButton.onClick.AddListener(HandlePlayClick);
        }

        private void HandlePlayClick()
        {
            OnPlayClicked?.Invoke();
        }

        public void Show()
        {
            menuPanel.SetActive(true);
        }

        public void Hide()
        {
            menuPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(HandlePlayClick);
        }
    }
}
