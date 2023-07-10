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
}