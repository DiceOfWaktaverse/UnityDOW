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
        private BattleStateMachine stateMachine;

        void Awake()
        {
            // 배틀스테이지 UI로딩
            UIManager.Instance.InitUI(eSceneType.BATTLE_STAGE);

            // 배틀 스테이지 스테이트 머신 초기화
            stateMachine = new BattleStateMachine(this);
            stateMachine.StateInit();

            // 이벤트 리스너 등록
            this.EventStartListening();
        }

        // 배틀 스테이지 업데이트에서 해야 할 일
        // - 뭔가 데이터 적인 일은 모두 스테이스머신과 유저인포가 알아서 할 예정
        // - 스테이스 머신의 업데이트가 완료되면 이 Update메서드에서 유저 인포를 받아와서 필요한 UI를 그림
        // - 혹시 유저가 설정 창을 켜거나 쇼핑을 누르면 팝업을 띄움, 멈출지 말지는 아직 안정했음
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
                    var cards = UserInfo.Instance.GetInfo<CardInfo>().Hand;
                    for (int i = 0; i < cards.Count; i++)
                    {
                        Debug.Log(cards[i]);
                    }

                    break;
                case eBattleStageEventType.ChapterMapOnClose:
                    stateMachine.ChangeState<StartingState>();
                    break;
            }
        }
    }
}