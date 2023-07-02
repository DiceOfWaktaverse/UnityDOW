using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class Mulligan : MonoBehaviour, EventListenerBase
    {

        [SerializeField, Range(1, 6)]
        public int MaxCardCount = 6;

        [SerializeField]
        public int IncludeCharacterCardCount = 1;

        [SerializeField, Range(1, 6)]
        public int MinimumCardCount = 2;

        public GameObject CardLayout = null;
        public GameObject CardTemplate = null;
        public GameObject MulliganButton = null;

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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PopupManager.ClosePopup<MulliganPopup>();
            }
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
            // TODO: save selected card list to userInfo

            PopupManager.ClosePopup<MulliganPopup>();
            EventManager.TriggerEvent(eBattleStageEventType.MulliganOnClose);
        }
    }
}
