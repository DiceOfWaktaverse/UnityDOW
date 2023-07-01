using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class Mulligan : MonoBehaviour
    {

        [SerializeField, Range(1, 6)]
        public int MaxCardCount = 6;

        [SerializeField]
        public int IncludeCharacterCardCount = 1;

        [SerializeField, Range(1, 6)]
        public int MinimumCardCount = 2;

        [SerializeField]
        public GameObject CardLayout = null;

        [SerializeField]
        public GameObject CardTemplate = null;

        private int currentCount = 0;

        private List<GameObject> cardList = new List<GameObject>();
        private bool initial = true;

        private List<string> characterCardPool = null;
        private List<string> otherCardPool = null;

        void Start()
        {
            // 카드 수를 정하고 그에 맞게 카드 게임오브젝트를 생성해둠
            currentCount = MaxCardCount;
            for (int i = 0; i < MaxCardCount; ++i)
            {
                GameObject card = Instantiate(CardTemplate, CardLayout.transform);
                cardList.Add(card);
            }
            CardTemplate.SetActive(false);

            characterCardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR });
            otherCardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.FIELD, eCardType.INST, eCardType.ITEM }, false);
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
            // TODO: replace this with real logic, including Seeding
            List<string> cardNoList = new List<string>();
            for (int i = 0, cardIndex = 0; i < currentCount; ++i)
            {
                if (IncludeCharacterCardCount > i) {
                    cardIndex = Random.Range(0, characterCardPool.Count);
                    cardList[i].GetComponent<CardUI>().LoadCardData(characterCardPool[cardIndex]);
                } else {
                    cardIndex = Random.Range(0, otherCardPool.Count);
                    cardList[i].GetComponent<CardUI>().LoadCardData(otherCardPool[cardIndex]);
                }
            }
        }
    }
}
