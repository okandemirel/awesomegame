namespace Modules.CardModule.Scripts.Core.Data
{
    public class CardModel
    {
        public int Id { get; private set; }
        public CardType CardType { get; private set; }
        public bool IsFlipped { get; set; }
        public bool IsMatched { get; set; }

        public CardModel(int id, CardType cardType)
        {
            Id = id;
            CardType = cardType;
            IsFlipped = false;
            IsMatched = false;
        }
    }
}