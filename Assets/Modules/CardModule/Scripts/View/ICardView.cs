using System;
using UnityEngine;

namespace Modules.CardModule.Scripts.View
{
    public interface ICardView
    {
        void Initialize(int id, Sprite frontSprite, Sprite backSprite, Color color);
        void Flip(bool showFront, float duration);
        void SetMatched();
        void SetInteractable(bool interactable);
        void SetPosition(Vector3 position);
        void SetSize(Vector2 size);
        void PlayMatchEffect();

        event Action<int> OnCardClicked;
    }
}