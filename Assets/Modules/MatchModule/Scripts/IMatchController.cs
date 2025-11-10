using Modules.CardModule.Scripts.Core.Data;

namespace Modules.MatchModule.Scripts
{
    public interface IMatchController
    {
        void Initialize();
        bool IsMatch(CardModel card1, CardModel card2);
    }
}