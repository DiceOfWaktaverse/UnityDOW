using System.Collections.Generic;

namespace DOW
{
    public enum eSlotType
    {
        None = 0,
        SLOT1,
        SLOT2,
        SLOT3,
        SLOT4,
        SLOT5,
    }

    [System.Serializable]
    public class CardInfo : IInfo
    {

        public List<Card> Coffin { get; protected set; } = new List<Card>();
        public List<Card> Hand { get; protected set; } = new List<Card>();
        public Dictionary<eSlotType, CharacterCard> Character { get; protected set; } = new Dictionary<eSlotType, CharacterCard>();

        public FieldCard FriendlyField { get; protected set; } = null;

        public CardInfo()
        {
            Initialize();
        }

        public void Initialize()
        {

        }

        public void SaveInfo()
        {

        }

        public void LoadInfo()
        {

        }

        public void AddCharacter(eSlotType slot, CharacterCard card)
        {
            Character.Add(slot, card);
        }
        public void AddHand(Card card)
        {
            Hand.Add(card);
        }
        public void AddHand(List<Card> cards)
        {
            Hand.AddRange(cards);
        }
    }
}
