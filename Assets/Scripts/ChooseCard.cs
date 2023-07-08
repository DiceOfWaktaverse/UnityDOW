using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace DOW
{
    public class ChooseCard : MonoBehaviour
    {
        private GameObject CardLayout = null;
        private GameObject CardTemplate = null;

        private List<GameObject> cardsLayout; 

        private List<Card> cardsData; 

        private void Awake()
        {
            CardLayout = transform.Find("Background/CardLayout").gameObject;
            CardTemplate = ResourceManager.GetResource<GameObject>(eResourcePath.PREFABS, "Card");

            cardsLayout = new List<GameObject>();

            cardsData = new List<Card>();

            ///¼ÀÇÃ
            addCards("1001", "1002");

            setCardsLayout();

        }

        private void Update()
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PopupManager.ClosePopup<ChooseCardPopup>();
            }
        }

        public void setCardsLayout()
        {
                Debug.Log(1);
            for (int i = 0; i < cardsData.Count; i++)
            {
                GameObject card = Instantiate(CardTemplate, CardLayout.transform);
                addEventTrigger(card);

                cardsLayout.Add(card);
            }
            CardTemplate.SetActive(false);

            openCardsLayout();
        }


        public void openCardsLayout()
        {
            for (int i = 0; i < cardsLayout.Count; i++)
            {
                cardsLayout[i].SetActive(true);
                cardsLayout[i].GetComponent<CardUI>().LoadCardData(cardsData[i]);
            }
        }


        public void clearCardLayout()
        {
            cardsLayout.Clear();
        }

        public void addEventTrigger(GameObject card)
        {
            EventTrigger trigger = card.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { onClickEvent(card); });
            trigger.triggers.Add(entry);
        }

        public void onClickEvent(GameObject card)
        {
            Debug.Log(card.GetComponent<CardUI>().card.Key);
        }

        public void addCards(params Card[] inputCards)
        {
            cardsData.AddRange(inputCards);
        }

        public void addCards(List<Card> inputCards)
        {
            cardsData.AddRange(inputCards);
        }

        public void addCards(params string[] inputCards)
        {
            cardsData.AddRange((
                from card in inputCards
                select CardManager.Get(card)).ToList());
        }
        public void addCards(List<string> inputCards)
        {
            cardsData.AddRange((
                from card in inputCards
                select CardManager.Get(card)).ToList());
        }

    }
}
