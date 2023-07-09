using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffin : MonoBehaviour
{
    [SerializeField]
    public GameObject CardLayout = null;
    public GameObject CardTemplate = null;

    private int currentCount = 0;

    private List<GameObject> cardList = new List<GameObject>();
    private List<Card> cardListCard = new List<Card>();
    //테스트용
    private List<string> cardPool = null;
    private void Awake()
    {
        // 레퍼런스를 가져옴
        CardTemplate = ResourceManager.GetResource<GameObject>(eResourcePath.PREFABS, "Card");
    }

    private void Start() {
        cardListCard = UserInfo.Instance.GetInfo<BattleInfo>().Coffin;       
            setCardLayout();
            setCardInfo(); 
    }
    private void OnEnable() {
        setCardLayout();
        setCardInfo(); 
    }
    public void setCardLayout()
    {
        Transform cardTransform = CardLayout.transform;
        Vector3 pos = cardTransform.position;
        for (int i = currentCount; i < UserInfo.Instance.GetInfo<BattleInfo>().Coffin.Count; i++)
        {
            GameObject card = Instantiate(CardTemplate, cardTransform);
            Debug.Log("card" + card);
            cardList.Add(card);
        }
        CardTemplate.SetActive(false);
    }

    public void setCardInfo()
    {
        for (int i = currentCount; i < UserInfo.Instance.GetInfo<BattleInfo>().Coffin.Count; i++)
        {
            cardList[i].SetActive(true);
            cardList[i].GetComponent<CardUI>().LoadCardData(cardListCard[i].Key);
        }
    }

    public void TestMoveCoffin() {
        cardPool = TableManager.GetTable<CardTable>().GetKeys(new List<eCardType> { eCardType.CHAR , eCardType.FIELD, eCardType.ITEM }, false);
        int cardIndex = Random.Range(0, cardPool.Count);
        Card testCard = CardManager.Get(cardPool[cardIndex]);
        Debug.Log(testCard.Label);
        UserInfo.Instance.GetInfo<BattleInfo>().Coffin.Add(testCard);
    }
}
