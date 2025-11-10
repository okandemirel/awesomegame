using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.CardModule.Scripts.View
{
    public class CardView : MonoBehaviour, ICardView
    {
        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup canvasGroup;

        private int _id;
        private bool _isFlipping;

        public event Action<int> OnCardClicked;

        private void Awake()
        {
            if (button != null)
            {
                button.onClick.AddListener(HandleClick);
            }
        }

        public void Initialize(int id, Sprite frontSprite, Sprite backSprite, Color color)
        {
            _id = id;
            if (frontImage != null) frontImage.sprite = frontSprite;
            if (backImage != null) backImage.sprite = backSprite;
            if (frontImage != null) frontImage.color = color;

            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);

            transform.rotation = Quaternion.identity;
            _isFlipping = false;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }

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
            SetInteractable(false);

            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = showFront ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

            float elapsed = 0;
            bool imagesSwitched = false;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                if (!imagesSwitched && t >= 0.5f)
                {
                    frontImage.gameObject.SetActive(showFront);
                    backImage.gameObject.SetActive(!showFront);
                    imagesSwitched = true;
                }

                yield return null;
            }

            transform.rotation = targetRotation;
            _isFlipping = false;
            SetInteractable(true);
        }

        public void SetMatched()
        {
            SetInteractable(false);
            if (canvasGroup != null)
            {
                StartCoroutine(FadeOut());
            }
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
            if (button != null)
            {
                button.interactable = interactable;
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetSize(Vector2 size)
        {
            var rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = size;
            }
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
            if (button != null)
            {
                button.onClick.RemoveListener(HandleClick);
            }
        }
    }
}