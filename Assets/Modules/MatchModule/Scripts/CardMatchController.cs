using Modules.CardModule.Scripts.Core.Data;

namespace Modules.MatchModule.Scripts
{
    public class CardMatchController : IMatchController
    {
        public void Initialize() { }

        public bool IsMatch(CardModel card1, CardModel card2)
        {
            if (card1 == null || card2 == null) return false;
            if (card1.Id == card2.Id) return false;

            return card1.CardType == card2.CardType;
        }
    }
}