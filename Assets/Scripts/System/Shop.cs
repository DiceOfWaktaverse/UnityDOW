using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class DumyCardData
    {
        public int cardId;
        public int cardCost;

        public DumyCardData(int cardId, int cardCost)
        {
            this.cardId = cardId;
            this.cardCost = cardCost;
        }
    }

    public static class ShopData
    {
        //deck으로 되어있는경우 cardDeck_1_Cost에서 통합적인 cost를 사용하고
        public static Stack<DumyCardData> cardDeck_1 = new Stack<DumyCardData>();
        public static Stack<DumyCardData> cardDeck_2 = new Stack<DumyCardData>();

        //card인 경우 card 자체의 cost를 가져와 사용한다
        public static DumyCardData card_1 = null;
        public static DumyCardData card_2 = null;
        public static DumyCardData card_3 = null;

        public static int cardDeck_1_Cost = 1;
        public static int cardDeck_2_Cost = 1;

        //임시로 사용중인 플레잉어 재화
        public static int coin;

    }

    public class Shop : MonoBehaviour 
    {
        private void Start()
        {
            changeProduct();
        }

        public static void buyCard(int num)
        {
            if (num == 1)
            {
                if(ShopData.card_1 != null && ShopData.coin >= ShopData.card_1.cardCost)
                {
                    ShopData.coin -= ShopData.card_1.cardCost;
                    Debug.Log("Card ID: " + ShopData.card_1.cardId);
                }
                else Debug.Log("can't buy or already sold");
            }
            else if (num == 2)
            {
                if(ShopData.card_2 != null && ShopData.coin >= ShopData.card_2.cardCost)
                {
                    ShopData.coin -= ShopData.card_2.cardCost;
                    Debug.Log("Card ID: " + ShopData.card_2.cardId);
                }
                else Debug.Log("can't buy or already sold");
            }
            else if (num == 3)
            {
                 if(ShopData.card_3 != null && ShopData.coin >= ShopData.card_3.cardCost)
                 {
                    ShopData.coin -= ShopData.card_3.cardCost;
                    Debug.Log("Card ID: " + ShopData.card_3.cardId);
                }
                else Debug.Log("can't buy or already sold");
            }
            else
            {
                Debug.LogAssertion("Unsuitable card num");
            }
        }

        public static void buyCardDeck(int num)
        {
            if(num == 1)
            {
                if (ShopData.coin >= ShopData.cardDeck_1_Cost)
                {
                    ShopData.coin -= ShopData.cardDeck_1_Cost;
                    Debug.Log("Card ID: " + ShopData.cardDeck_1.Pop().cardId + "Cost: " + ShopData.cardDeck_1_Cost);
                    ShopData.cardDeck_1_Cost += 2;
                }
                else Debug.Log("lack of coin");
            }
            else if (num == 2)
            {
                if (ShopData.coin >= ShopData.cardDeck_2_Cost)
                {
                    ShopData.coin -= ShopData.cardDeck_2_Cost;
                    Debug.Log("Card ID: " + ShopData.cardDeck_2.Pop().cardId + "Cost: " + ShopData.cardDeck_2_Cost);
                    ShopData.cardDeck_2_Cost += 2;
                }
                else Debug.Log("lack of coin");
            }
            else
            {
                Debug.LogError("Unsuitable deck num");
            }
        }

        public static void resetCost()
        {
            ShopData.cardDeck_1_Cost = 1;
            ShopData.cardDeck_2_Cost = 1;
            Debug.Log("resetCost");
        }

        //우선 random으로 만들어둠
        public static void changeProduct()
        {
            ShopData.cardDeck_1.Clear();
            for (int i = 0; i < 30; i++)
                ShopData.cardDeck_1.Push(new DumyCardData(i, Random.Range(1, 15)));

            ShopData.cardDeck_2.Clear();
            for (int i = 0; i < 30; i++)
                ShopData.cardDeck_2.Push(new DumyCardData(30 + i, Random.Range(1, 15)));

            ShopData.card_1 = new DumyCardData(Random.Range(1,60),Random.Range(1,15));
            ShopData.card_2 = new DumyCardData(Random.Range(1,60),Random.Range(1,15));
            ShopData.card_3 = new DumyCardData(Random.Range(1,60),Random.Range(1,15));
        }

        public static void getCoin()
        {
            ShopData.coin += 10;
            Debug.Log("Coin: " + ShopData.coin);
        }
    }
}
