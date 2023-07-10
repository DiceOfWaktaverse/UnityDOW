using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public static class ShopData
    {
        //deck으로 되어있는경우 cardDeck_1_Cost에서 통합적인 cost를 사용하고
        public static List<string> cardDeck_1 = new List<string>();
        public static List<string> cardDeck_2 = new List<string>();

        //card인 경우 card 자체의 cost를 가져와 사용한다
        public static List<GameObject> cardList = new List<GameObject>();
        public static List<Card> cardListCard = new List<Card>();

        public static int cardDeck_1_Cost = 1;
        public static int cardDeck_2_Cost = 1 ;

        public readonly static int CardCost = 3; 

        //임시로 사용중인 플레잉어 재화
        public static int coin;

    }

    public class ShopPopup : Popup<ShopPopup>
    {
        public const int MaxCardCount = 3;

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
            CardTemplate = ResourceManager.GetResource<GameObject>(eResourcePath.PREFABS, "Card");

            cardLayoutList.Add(card1Layout);
            cardLayoutList.Add(card2Layout);
            cardLayoutList.Add(card3Layout);

            setCardLayout();
            Random.InitState(System.DateTime.Now.Millisecond);
        }

        private void Start()
        {
            setCardDeckInfo();
            setCardInfo();
        }

        public void setCardLayout()
        {
            for (int i = 0; i < MaxCardCount; i++)
            {
                ShopData.cardList.Add(Instantiate(CardTemplate, cardLayoutList[i].transform));
                ShopData.cardListCard.Add(null);
            }
            CardTemplate.SetActive(false);
        }

        public void setCardInfo()
        {
            cardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR , eCardType.FIELD, eCardType.ITEM }, false);
            for (int i = 0;i < MaxCardCount;i++)
            {
                ShopData.cardList[i].SetActive(true);   
                int cardIndex = Random.Range(0, cardPool.Count);
                ShopData.cardListCard[i] = CardManager.Get(cardPool[cardIndex]);
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
                UserInfo.Instance.GetInfo<BattleInfo>().Hand.Add(ShopData.cardListCard[num]);

                //Debug.Log(UserInfo.Instance.GetInfo<BattleInfo>().Hand[UserInfo.Instance.GetInfo<BattleInfo>().Hand.Count - 1]);
                Debug.Log(UserInfo.Instance.GetInfo<BattleInfo>().Hand.Count);
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

                    UserInfo.Instance.GetInfo<BattleInfo>().Hand.Add(CardManager.Get(ShopData.cardDeck_1[cardIndex]));
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
                    
                    UserInfo.Instance.GetInfo<BattleInfo>().Hand.Add(CardManager.Get(ShopData.cardDeck_1[cardIndex]));
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

        public static void getCoin()
        {
            ShopData.coin += 10;
            Debug.Log("Coin: " + ShopData.coin);
        }

        //popup part
        public static ShopPopup OpenPopup()
        {
            ShopPopup popup = PopupManager.OpenPopup<ShopPopup>("ShopPopup");
            return popup;
        }

        public override void Initialize()
        {
        }

        public override void InitializeUI()
        {
        }

        public override void Refresh()
        {
        }
    }
}
