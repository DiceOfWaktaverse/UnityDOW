using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleStage : MonoBehaviour
    {
        private BattleStateMachine stateMachine;
        private BattleStageUI battleStageUI;

        void Awake()
        {
            // 배틀스테이지 UI로딩
            UIManager.Instance.InitUI(eSceneType.BATTLE_STAGE);
            battleStageUI = UIManager.Instance.Beacon.UIObjects.Find(x => x is BattleStageUI) as BattleStageUI;

            // 배틀 스테이지 스테이트 머신 초기화
            stateMachine = new BattleStateMachine(this);
            stateMachine.StateInit();
        }

        // 배틀 스테이지 업데이트에서 해야 할 일
        // - 뭔가 데이터 적인 일은 모두 스테이스머신과 유저인포가 알아서 할 예정
        // - 스테이스 머신의 업데이트가 완료되면 이 Update메서드에서 유저 인포를 받아와서 필요한 UI를 그림
        // - 혹시 유저가 설정 창을 켜거나 쇼핑을 누르면 팝업을 띄움, 멈출지 말지는 아직 안정했음
        void Update()
        {
            // 스테이트가 없으면 끝냄
            if (stateMachine.CurState == null) return;

            // 현재 스테이트 업데이트를 실행하고 결과값을 받음
            bool continueState = stateMachine.CurState.Update(Time.deltaTime);

            // 결과값이 true면 별 다른 일 없이 계속된다
            if (continueState == true) return;

            // 결과값이 false면 스테이트를 바꿔야 한다
            if (stateMachine.CurState is StartingState) {
                bool newGame = UserInfo.Instance.GetInfo<GameInfo>().IsNewGame;
                if (newGame == true) stateMachine.ChangeState<InitialMulliganState>();
                else stateMachine.ChangeState<ChapterInfoState>();
            } else if (stateMachine.CurState is InitialMulliganState) {
                stateMachine.ChangeState<ChapterInfoState>();
            } else if (stateMachine.CurState is ChapterInfoState) {
                stateMachine.ChangeState<BattleStartState>();
            }

        }

        void OnDestroy() { }

        public BattleStageUI GetUI() { return battleStageUI; }
    }
}