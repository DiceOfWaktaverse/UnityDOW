using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public enum eBattleStageEventType
    {
        MulliganOnClose,
        ChapterMapOnClose,
    }

    public class BattleStage : MonoBehaviour, EventListener<eBattleStageEventType>
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

        public void OnEvent(eBattleStageEventType eventType)
        {
            switch (eventType)
            {
                case eBattleStageEventType.MulliganOnClose:
                    stateMachine.ChangeState<ChapterInfoState>();
                    break;
                case eBattleStageEventType.ChapterMapOnClose:
                    stateMachine.ChangeState<StartingState>();
                    break;
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