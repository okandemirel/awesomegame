using System.Collections.Generic;
using Modules.CardModule.Scripts.View;
using UnityEngine;

namespace Modules.CardModule.Scripts
{
    public class CardPool : MonoBehaviour
    {
        [SerializeField] private List<CardView> pool = new List<CardView>();

        private int _nextAvailableIndex = 0;

        public void Initialize(int capacity)
        {
            _nextAvailableIndex = 0;
            foreach (var card in pool)
            {
                if (card != null)
                {
                    card.gameObject.SetActive(false);
                }
            }
        }

        public void RegisterCard(CardView card)
        {
            if (!pool.Contains(card))
            {
                pool.Add(card);
                card.gameObject.SetActive(false);
            }
        }

        public CardView GetCard()
        {
            if (_nextAvailableIndex < pool.Count)
            {
                var card = pool[_nextAvailableIndex];
                _nextAvailableIndex++;
                card.gameObject.SetActive(true);
                return card;
            }
            return null;
        }

        public void ReturnAll()
        {
            _nextAvailableIndex = 0;
            foreach (var card in pool)
            {
                if (card != null)
                {
                    card.gameObject.SetActive(false);
                }
            }
        }

        public int Capacity => pool.Count;
        public int ActiveCount => _nextAvailableIndex;
    }
}