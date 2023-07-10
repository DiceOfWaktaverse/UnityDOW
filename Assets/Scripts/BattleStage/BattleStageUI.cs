using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public class BattleStageUI : UIObject
    {

        [SerializeField, Range(1, 40), Tooltip("최대 소지 가능한 카드 갯수")]
        int MaxHandCount = 12;

        private List<GameObject> hand = new List<GameObject>();
        private int endPtr = 0;

        private GameObject handLayout;

        public void Awake() {
            handLayout = transform.Find("LowerPannel/HandLayout").gameObject;
            GameObject card = ResourceManager.GetResource<GameObject>(eResourcePath.PREFABS, "Card");
            while (hand.Count < MaxHandCount - 1) {
                GameObject cardUI = Instantiate(card, handLayout.transform);
                cardUI.SetActive(false);
                hand.Add(cardUI);
            }
        }

        public override void InitializeUI(eSceneType targetType) {
            if (eSceneType.BATTLE_STAGE == targetType)
            {
                curSceneType = targetType;
                curUIType = eSceneType.BATTLE_STAGE;
                ReuseAnim();
            }
            else
            {
                curSceneType = targetType;
                curUIType = eSceneType.NONE;
                UnuseAnim();
            }
        }

        public void LoadHand() {
            for (int i = 0; i < hand.Count; i++) {
                hand[i].SetActive(false);
            }
            List<Card> curHand = UserInfo.Instance.GetInfo<BattleInfo>().Hand;
            
            for (int i = 0; i < curHand.Count; i++) {
                Debug.Log(curHand[i]);
                hand[i].SetActive(true);
                hand[i].GetComponent<CardUI>().LoadCardData(curHand[i]);
            }
        }

        public static void OnClickShop()
        {
            ShopPopup.OpenPopup();
        }
        public static void OnClickCoffin()
        {
            CoffinPopup.OpenPopup();
        }
        public static void OnClickPreference()
        {
            BattlestageSettingPopup.OpenPopup();
        }

        public static void OnClickTurnEnd()
        {
            BattleEndPopup.OpenPopup();
        }

        public override void RefreshUI() {

        }

        public override bool RefreshUI(eSceneType targetType)
        {
            return base.RefreshUI(targetType);
        }
    }

}