using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{


    public class BattleStateMachine : SimpleStateMachine<StateBase>
    {
        public BattleStage battleStage;

        public override void SetState()
        {
            AddState(new StartingState());
            AddState(new InitialMulliganState());
            AddState(new ChapterInfoState());
            AddState(new EventState());
            AddState(new BattleStartState(battleStage));
            AddState(new BattleUserTurnStartState());
            AddState(new BattleUserDiceRollState());
            AddState(new BattleUserActionState());
            AddState(new BattleUserTurnEndState());
            AddState(new BattleEnemyTurnStartState());
            AddState(new BattleEnemyActionState());
            AddState(new BattleEnemyTurnEndState());
            AddState(new BattleEndState());

            // Set Initial State
            ChangeState<StartingState>();
        }

        public BattleStateMachine(BattleStage battleStage)
        {
            this.battleStage = battleStage;
        }
    }

    // 게임 인포 초기화 하는 스테이지, 새로하기면 완전 초기화, 이어서 하기면 로드를 함
    public class StartingState : StateBase
    {

        public override bool OnEnter()
        {

            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            GameInfo info = UserInfo.Instance.GetInfo<GameInfo>();
            if (info.IsNewGame == true) info.InitializeGameInfo();
            else info.LoadGameInfo();

            return false;
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    // 게임이 처음 시작되고 멀리건을 하는 단계, 만약 유저인포의 progress가 0이면 처음 시작하는 것이고, 0이 아니면 이어서 하는 것이라서 이 스테이트 넘김 
    public class InitialMulliganState : StateBase
    {
        MulliganPopup mulliganPopup;
        public override bool OnEnter()
        {
            mulliganPopup = PopupManager.OpenPopup<MulliganPopup>("MulliganPopup");
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            // 팝업이 닫히면 다음 스테이트로 넘어감
            if(mulliganPopup.gameObject.activeSelf == false)
            {
                return false;
            }

            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class ChapterInfoState : StateBase
    {
        ChapterMapPopup chapterMapPopup;

        public override bool OnEnter()
        {
            chapterMapPopup = PopupManager.OpenPopup<ChapterMapPopup>("ChapterMapPopup");
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            // 팝업이 닫히면 다음 스테이트로 넘어감
            if(chapterMapPopup.gameObject.activeSelf == false)
            {
                return false;
            }
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }

    }

    public class EventState : StateBase
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    // 배틀 스테이트의 베이스 스테이트, 사용하지 않는다. 배틀스테이트의 공통 정보를 여기에 정의하고 상속받아서 사용한다.
    public class BattleState : StateBase
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    // 배틀인포를 초기화 함, 게임인포를 로딩해야함
    public class BattleStartState : BattleState
    {
        private BattleStage battleStage;

        public BattleStartState(BattleStage battleStage) {
            this.battleStage = battleStage;
        }

        public override bool OnEnter()
        {
            UserInfo.Instance.GetInfo<BattleInfo>().ForkGameInfo();
            battleStage.GetUI().LoadHand();
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleUserTurnStartState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleUserDiceRollState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleUserActionState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleUserTurnEndState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }

        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleEnemyTurnStartState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            return base.Update(dt);
        }
        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleEnemyActionState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }
        public override bool Update(float dt)
        {
            return base.Update(dt);
        }
        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleEnemyTurnEndState : BattleState
    {
        public override bool OnEnter()
        {
            return base.OnEnter();
        }
        public override bool Update(float dt)
        {
            return base.Update(dt);
        }
        public override bool OnExit()
        {
            return base.OnExit();
        }
    }

    public class BattleEndState : BattleState
    {
        public override bool OnEnter()
        {
            UserInfo.Instance.GetInfo<BattleInfo>().MergeGameInfoAndClear();
            return base.OnEnter();
        }
        public override bool Update(float dt)
        {
            return base.Update(dt);
        }
        public override bool OnExit()
        {
            return base.OnExit();
        }
    }
}