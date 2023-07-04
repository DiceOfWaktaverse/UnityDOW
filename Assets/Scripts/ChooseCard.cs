using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        }

        public void OnEnable()
        {
            for (int i = 0; i < cardsData.Count; i++)
            {
                GameObject card = Instantiate(CardTemplate, CardLayout.transform);
                cardsLayout.Add(card);
            }
            CardTemplate.SetActive(false);

            openCardsLayout();
        }

        public void openCardsLayout()
        {
            for (int i = 0; i < cardsLayout.Count; i++)
            {
                cardsLayout[i].GetComponent<CardUI>().LoadCardData(cardsData[i]);
            }
        }

        public void addCards(params Card[] inputCards)
        {
            cardsData.AddRange(inputCards);
        }

        public void addCards(List<Card> inputCards)
        {
            cardsData.AddRange((IEnumerable<Card>)inputCards);
        }

        public void addCards(params string[] inputCards)
        {
            cardsData.AddRange((List<Card>)
                from card in inputCards
                select CardManager.Get(card));
        }
        public void addCards(List<string> inputCards)
        {
            cardsData.AddRange((List<Card>)
                from card in inputCards
                select CardManager.Get(card));
        }

    }
}
