using System;
using System.Collections.Generic;

namespace Modules.InputModule.Scripts
{
    public class CardInputHandler : IInputHandler
    {
        private readonly List<int> _selectedCards = new List<int>();
        private readonly HashSet<int> _cardsInComparison = new HashSet<int>();
        public event Action<int, int> OnTwoCardsSelected;

        public void Initialize()
        {
            _selectedCards.Clear();
            _cardsInComparison.Clear();
        }

        public bool? RegisterCardClick(int cardId)
        {
            if (_cardsInComparison.Contains(cardId)) return null;

            if (_selectedCards.Contains(cardId))
            {
                _selectedCards.Remove(cardId);
                return false;
            }

            _selectedCards.Add(cardId);

            if (_selectedCards.Count == 2)
            {
                _cardsInComparison.Add(_selectedCards[0]);
                _cardsInComparison.Add(_selectedCards[1]);

                OnTwoCardsSelected?.Invoke(_selectedCards[0], _selectedCards[1]);
                _selectedCards.Clear();
            }

            return true;
        }

        public void Clear()
        {
            _cardsInComparison.Clear();
        }
    }
}