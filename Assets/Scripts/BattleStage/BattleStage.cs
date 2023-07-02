using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum BattleStageEventType
    {
        InitialMulliganFinished,
    }

    public class BattleStage : MonoBehaviour, EventListener<BattleStageEventType>
    {
        private BattleStateMachine stateMachine = new BattleStateMachine();

        void Start()
        {
            stateMachine.StateInit();
            this.EventStartListening();
        }

        void Update()
        {
            if (stateMachine.CurState != null)
                stateMachine.CurState.Update(Time.deltaTime);
        }

        void OnDestroy()
        {
            this.EventStopListening();
        }

       public void OnEvent(BattleStageEventType eventType)
        {
            Debug.Log("InitialMulliganState OnEvent : " + eventType);
            if (eventType == BattleStageEventType.InitialMulliganFinished)
            {
                PopupManager.ClosePopup<MulliganPopup>();
            }
        }

        public static void OnClickShop()
        {
            ShopPopup.OpenPopup();
        }

        public static void OnClickPreference()
        {
            BattlestageSettingPopup.OpenPopup();
        }

        public static void OnClickTurnEnd()
        {
            BattleEndPopup.OpenPopup();
        }
    }
}