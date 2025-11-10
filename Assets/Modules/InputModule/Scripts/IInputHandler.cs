using System;

namespace Modules.InputModule.Scripts
{
    public interface IInputHandler
    {
        void Initialize();
        bool? RegisterCardClick(int cardId);
        event Action<int, int> OnTwoCardsSelected;
        void Clear();
    }
}