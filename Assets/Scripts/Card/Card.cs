using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class Card
    {
        // 비활성 상태의 데이터, 베이스 데이터이다.
        public string Key { get; protected set; } = "";
        public CardPack CardPack { get; protected set; } = null;
        public eCardType Type { get; protected set; } = eCardType.NONE;
        public string Illust { get; protected set; } = "9999";
        public string Label { get; protected set; } = "";
        public string Description { get; protected set; } = "";
        public List<Tag> Tags { get; protected set; } = new List<Tag>();
        public List<eRestriction> Restrictions { get; protected set; } = new List<eRestriction>();
        public List<Skill> Skills { get; protected set; } = new List<Skill>();

        // 활성 상태 여부, 활성 상태는 손패, 관, 필드에 존재하여 조작하거나 상태이상이 붙거나 체력이 까이거나 할 수 있는 상태를 의미 한다.
        public bool active = false;

        public Card(CardData datum)
        {
            setBaseData(datum);

            active = false;
        }

        public override string ToString()
        {
            string str = "";
            str += Key + "(" + Type + "): " + Label;
            return str;
        }

        private void setBaseData(CardData datum) {
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
    }

    public static class CardFactory
    {
        public static Card Create(string key)
        {
            CardData datum = CardData.Get(key);
            Card card = null;

            if (datum == null)
                throw new System.Exception("Invalid card key");

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
