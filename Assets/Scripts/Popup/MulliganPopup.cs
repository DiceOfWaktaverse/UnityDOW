using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{

    public class MulliganPopup : Popup<MulliganPopup>
    {
        [SerializeField, Range(1, 6)]
        public int MaxCardCount = 6;

        [SerializeField, Min(1)]
        public int IncludeCharacterCardCount = 1;

        [SerializeField, Range(1, 6)]
        public int MinimumCardCount = 2;

        private GameObject CardLayout = null;
        private GameObject CardTemplate = null;
        private GameObject MulliganButton = null;

        private int currentCount = 0;

        private List<GameObject> cardList = new List<GameObject>();
        private List<Card> selectedCardList = new List<Card>();
        private bool initial = true;

        private List<string> characterCardPool = null;
        private List<string> otherCardPool = null;

        void Awake()
        {
            // 필요한 레퍼런스를 가져옴 
            CardLayout = transform.Find("Background/CardLayout").gameObject;
            CardTemplate = ResourceManager.GetResource<GameObject>(eResourcePath.PREFABS, "Card");
            MulliganButton = transform.Find("Background/ButtonLayout/btn_Mulligan").gameObject;

            // 카드 수를 정하고 그에 맞게 카드 게임오브젝트를 생성해둠
            currentCount = MaxCardCount;
            for (int i = 0; i < MaxCardCount; i++)
            {
                GameObject card = Instantiate(CardTemplate, CardLayout.transform);
                selectedCardList.Add(null);
                cardList.Add(card);
            }
            CardTemplate.SetActive(false);

            characterCardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR });
            otherCardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.FIELD, eCardType.ITEM }, false);
            Random.InitState(System.DateTime.Now.Millisecond);
            CardMulligan();
            initial = false;
        }

        void Update()
        {

        }

        public static MulliganPopup OpenPopup()
        {
            MulliganPopup popup = PopupManager.OpenPopup<MulliganPopup>("MulliganPopup");
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

        public void CardMulligan()
        {
            if (currentCount > MinimumCardCount && !initial)
            {
                currentCount--;
                cardList[currentCount].SetActive(false);
            }

            // random select int
            for (int i = 0, cardIndex = 0; i < currentCount; ++i)
            {
                // 캐릭터 카드 최소 수치를 채움
                if (IncludeCharacterCardCount > i)
                {
                    cardIndex = Random.Range(0, characterCardPool.Count);
                    selectedCardList[i] = CardManager.Get(characterCardPool[cardIndex]);
                    cardList[i].GetComponent<CardUI>().LoadCardData(selectedCardList[i]);
                }
                // 이외의 카드를 로드함
                else
                {
                    cardIndex = Random.Range(0, otherCardPool.Count);
                    selectedCardList[i] = CardManager.Get(otherCardPool[cardIndex]);
                    cardList[i].GetComponent<CardUI>().LoadCardData(selectedCardList[i]);
                }
            }

            if (currentCount == MinimumCardCount)
            {
                DisableMulligan();
            }
        }

        public void DisableMulligan()
        {
            MulliganButton.GetComponent<Button>().interactable = false;
        }

        public void EndMulligan()
        {
            for (int i = 0, slot = 0; i < selectedCardList.Count; i++)
            {
                if (selectedCardList[i] is CharacterCard characterCard)
                {
                    UserInfo.Instance.GetInfo<GameInfo>().AddCharacter(eSlotType.SLOT1 + slot++, characterCard);
                } else {
                    UserInfo.Instance.GetInfo<GameInfo>().AddHand(selectedCardList[i]);
                }
            }

            PopupManager.ClosePopup<MulliganPopup>();
        }
    }
}
