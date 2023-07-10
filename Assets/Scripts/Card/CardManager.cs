using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class CardManager : Singleton<CardManager>, IManagerBase
    {

        private static Dictionary<string, Card> cardMap = new Dictionary<string, Card>();

        public static Card Get(string key) {
            if (!cardMap.ContainsKey(key)) {
                cardMap.Add(key, CardFactory.Create(key));
            }
            return cardMap[key];
        }

        public override void Initialize() {

        }

        public void Update(float dt) {

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