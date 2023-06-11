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
        public GameObject CardLayout = null;

        [SerializeField]
        public GameObject CardTemplate = null;


        private int currentCount = 0;

        private List<GameObject> cardList = new List<GameObject>();
        private bool initial = true;

        void Start()
        {
            currentCount = MaxCardCount;
            for (int i = 0; i < MaxCardCount; ++i)
            {
                GameObject card = Instantiate(CardTemplate, CardLayout.transform);
                cardList.Add(card);
            }
            CardTemplate.SetActive(false);
            // TODO: replace with real seed
            Random.InitState(0);
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

        public void CardMulligan() {
            if (currentCount > 1 && !initial) {
                currentCount--;
                cardList[currentCount].SetActive(false);
            }

            // random select int
            // TODO: replace this with real logic, including Seeding
            List<int> cardNoList = new List<int>();
            for (int i = 0; i < currentCount; ++i)
            {
                cardNoList.Add(Random.Range(0, 200));
            }

            // set card
            // TODO: repace this with real card
            for (int i = 0; i < currentCount; ++i)
            {
                cardList[i].GetComponentInChildren<Text>().text = "Card " + cardNoList[i].ToString();
            }
        }
    }
}
