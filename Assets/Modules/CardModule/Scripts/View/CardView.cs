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
            SetInteractable(true);
        }

        public void Flip(bool showFront, float duration)
        {
            if (_isFlipping) return;
            StartCoroutine(FlipAnimation(showFront, duration));
        }

        private IEnumerator FlipAnimation(bool showFront, float duration)
        {
            _isFlipping = true;
            SetInteractable(false);

            float elapsed = 0;
            Quaternion startRotation = transform.rotation;
            Quaternion midRotation = startRotation * Quaternion.Euler(0, 90, 0);
            Quaternion endRotation = startRotation * Quaternion.Euler(0, 180, 0);

            while (elapsed < duration / 2)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (duration / 2);
                transform.rotation = Quaternion.Lerp(startRotation, midRotation, t);
                yield return null;
            }

            frontImage.gameObject.SetActive(showFront);
            backImage.gameObject.SetActive(!showFront);

            elapsed = 0;
            while (elapsed < duration / 2)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / (duration / 2);
                transform.rotation = Quaternion.Lerp(midRotation, endRotation, t);
                yield return null;
            }

            transform.rotation = showFront ? endRotation : startRotation;
            _isFlipping = false;
            SetInteractable(true);
        }

        public void SetMatched()
        {
            StartCoroutine(DeactivateAfterEffect());
        }

        private IEnumerator DeactivateAfterEffect()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
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