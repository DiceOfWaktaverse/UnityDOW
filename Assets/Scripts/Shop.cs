using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DOW
{
    public static class ShopData
    {
        //deck���� �Ǿ��ִ°�� cardDeck_1_Cost���� �������� cost�� ����ϰ�
        public static List<string> cardDeck_1 = new List<string>();
        public static List<string> cardDeck_2 = new List<string>();

        //card�� ��� card ��ü�� cost�� ������ ����Ѵ�
        public static List<GameObject> cardList = new List<GameObject>();

        public static int cardDeck_1_Cost = 1;
        public static int cardDeck_2_Cost = 1 ;

        public readonly static int CardCost = 3; 

        //�ӽ÷� ������� �÷��׾� ��ȭ
        public static int coin;

    }

    public class Shop : MonoBehaviour 
    {
        public const int MaxCardCount = 3;

        [SerializeField]
        public GameObject CardTemplate = null;

        [SerializeField]
        public GameObject cardDeck1Layout;
        [SerializeField]
        public GameObject cardDeck2Layout;

        [SerializeField]
        public GameObject card1Layout;
        [SerializeField]
        public GameObject card2Layout;
        [SerializeField]
        public GameObject card3Layout;

        private List<GameObject> cardLayoutList = new List<GameObject>();

        private List<string> cardDeckPool = null;
        private List<string> cardPool = null;

        private void Awake()
        {
            cardLayoutList.Add(card1Layout);
            cardLayoutList.Add(card2Layout);
            cardLayoutList.Add(card3Layout);

            setCardLayout();
        }

        private void Start()
        {

            changeProduct();


            Random.InitState(System.DateTime.Now.Millisecond);

            setCardInfo();
            setCardDeckInfo();
        }

        public void setCardLayout()
        {
            for (int i = 0; i < MaxCardCount; i++)
            {
                ShopData.cardList.Add(Instantiate(CardTemplate, cardLayoutList[i].transform));
            }
            Debug.Log("setcardlayout");
            CardTemplate.SetActive(false);
        }

        public void setCardInfo()
        {
            cardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR , eCardType.FIELD, eCardType.ITEM }, false);
            for (int i = 0;i < MaxCardCount;i++)
            {
                cardLayoutList[i].SetActive(true);
                int cardIndex = Random.Range(0, cardPool.Count);
                ShopData.cardList[i].GetComponent<CardUI>().LoadCardData(cardPool[cardIndex]);
            }
        }

        public void setCardDeckInfo()
        {
            ShopData.cardDeck_1 = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR , eCardType.FIELD, eCardType.INST ,eCardType.ITEM }, false);
            ShopData.cardDeck_2 = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR , eCardType.FIELD, eCardType.INST ,eCardType.ITEM }, false);
        }

        public void buyCard(int num)
        {
            num -= 1;
            if (ShopData.coin >= ShopData.CardCost)
            {
                ShopData.coin -= ShopData.CardCost;
                cardLayoutList[num].gameObject.SetActive(false);
            }
            else
                Debug.Log("can't buy or already sold");
        }

        public static void buyCardDeck(int num)
        {
            if(num == 1)
            {
                if (ShopData.coin >= ShopData.cardDeck_1_Cost)
                {
                    int cardIndex = Random.Range(0, ShopData.cardDeck_1.Count);
                    ShopData.coin -= ShopData.cardDeck_1_Cost;

                    Debug.Log(ShopData.cardDeck_1[cardIndex]);
                    ShopData.cardDeck_1.Remove(ShopData.cardDeck_1[cardIndex]);

                    ShopData.cardDeck_1_Cost += 2;
                }
                else Debug.Log("lack of coin");
            }
            else if (num == 2)
            {
                if (ShopData.coin >= ShopData.cardDeck_2_Cost)
                {
                    int cardIndex = Random.Range(0, ShopData.cardDeck_2.Count);
                    ShopData.coin -= ShopData.cardDeck_2_Cost;
                    
                    Debug.Log(ShopData.cardDeck_2[cardIndex]);
                    ShopData.cardDeck_2.Remove(ShopData.cardDeck_2[cardIndex]);

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

        //�켱 random���� ������
        public void changeProduct()
        {
            setCardInfo();
            setCardDeckInfo();
        }

        public static void getCoin()
        {
            ShopData.coin += 10;
            Debug.Log("Coin: " + ShopData.coin);
        }
    }
}
