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
        //deck���� �Ǿ��ִ°�� cardDeck_1_Cost���� �������� cost�� ����ϰ�
        public static List<DumyCardData> cardDeck_1 = null;
        public static List<DumyCardData> cardDeck_2 = null;

        //card�� ��� card ��ü�� cost�� ������ ����Ѵ�
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
