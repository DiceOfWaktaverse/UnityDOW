using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class DumyCardData
    {
        int cardId;
        int cardCost;
    }

    public static class ShopData
    {
        //deck으로 되어있는경우 cardDeck_1_Cost에서 통합적인 cost를 사용하고
        public static List<DumyCardData> cardDeck_1 = null;
        public static List<DumyCardData> cardDeck_2 = null;

        //card인 경우 card 자체의 cost를 가져와 사용한다
        public static DumyCardData card_1 = null;
        public static DumyCardData card_2 = null;
        public static DumyCardData card_3 = null;

        public static int cardDeck_1_Cost = 1;
        public static int cardDeck_2_Cost = 1;

    }

    public class Shop : Singleton<Shop>
    {

        public void resetCost()
        {
            ShopData.cardDeck_1_Cost = 1;
            ShopData.cardDeck_2_Cost = 1;
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
