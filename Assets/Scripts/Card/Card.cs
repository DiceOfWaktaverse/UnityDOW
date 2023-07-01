using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class Card
    {
        public string Key { get; protected set; } = "";
        public CardPack CardPack { get; protected set; } = null;
        public eCardType Type { get; protected set; } = eCardType.NONE;
        public string Illust { get; protected set; } = "9999";
        public string Label { get; protected set; } = "";
        public string Description { get; protected set; } = "";
        public List<Tag> Tags { get; protected set; } = new List<Tag>();
        public List<eRestriction> Restrictions { get; protected set; } = new List<eRestriction>();
        public List<Skill> Skills { get; protected set; } = new List<Skill>();

        public Card(CardData datum)
        {
            Key = datum.GetKey();
            CardPack = new CardPack(datum.CardPack);
            Type = datum.Type;
            Illust = datum.Illust;
            Label = datum.Label;
            Description = datum.Description;

            for (int i = 0; i < datum.Tags.Count; i++)
            {
                Tags.Add(new Tag(datum.Tags[i]));
            }

            Restrictions = datum.Restrictions;
            Skills = datum.Skills.Select(x => new Skill(x)).ToList();
        }

        public override string ToString()
        {
            string str = "";
            str += Key + "(" + Type + "): " + Label;
            return str;
        }
    }

    public static class CardFactory
    {
        public static Card Create(string key)
        {
            CardData datum = CardData.Get(key);
            Card card = null;

            switch (datum.Type)
            {
                case eCardType.CHAR:
                    card = new CharacterCard(datum);
                    break;
                case eCardType.FIELD:
                    card = new FieldCard(datum);
                    break;
                case eCardType.INST:
                    card = new InstantCard(datum);
                    break;
                case eCardType.ITEM:
                    card = new ItemCard(datum);
                    break;
                default:
                    throw new System.Exception("Invalid card type");
            }
            return card;
        }
    }
}
