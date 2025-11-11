using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.CardModule.Scripts.View
{
    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private Transform visualContainer;
        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup canvasGroup;

        private int _id;
        private bool _isFlipping;

        public event Action<int> OnCardClicked;

        private void Awake()
        {
            button.onClick.AddListener(HandleClick);
        }

        public void Initialize(int id, Sprite frontSprite, Sprite backSprite, Color color)
        {
            _id = id;
            frontImage.sprite = frontSprite;
            backImage.sprite = backSprite;
            frontImage.color = color;

            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);

            visualContainer.localRotation = Quaternion.identity;
            canvasGroup.alpha = 1f;
            _isFlipping = false;

            gameObject.SetActive(true);
            SetInteractable(true);
        }

        public void Flip(bool showFront, float duration)
        {
            if (_isFlipping)
            {
                StopAllCoroutines();
                _isFlipping = false;
            }
            StartCoroutine(FlipAnimation(showFront, duration));
        }

        private IEnumerator FlipAnimation(bool showFront, float duration)
        {
            _isFlipping = true;

            Quaternion startRotation = visualContainer.localRotation;
            Quaternion targetRotation = showFront ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

            float elapsed = 0;
            bool imagesSwitched = false;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                visualContainer.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

                if (!imagesSwitched && t >= 0.5f)
                {
                    frontImage.gameObject.SetActive(showFront);
                    backImage.gameObject.SetActive(!showFront);
                    imagesSwitched = true;
                }

                yield return null;
            }

            visualContainer.localRotation = targetRotation;
            _isFlipping = false;
        }

        public void SetMatched()
        {
            SetInteractable(false);
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float duration = 0.5f;
            float elapsed = 0;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = 1f - (elapsed / duration);
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }

        public void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetSize(Vector2 size)
        {
            ((RectTransform)transform).sizeDelta = size;
        }

        public void PlayMatchEffect()
        {
            StartCoroutine(ScalePulse());
        }

        private IEnumerator ScalePulse()
        {
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.2f;
            float duration = 0.3f;
            float elapsed = 0;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                transform.localScale = Vector3.Lerp(originalScale, targetScale, Mathf.Sin(t * Mathf.PI));
                yield return null;
            }

            transform.localScale = originalScale;
        }

        private void HandleClick()
        {
            OnCardClicked?.Invoke(_id);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(HandleClick);
        }
    }
}