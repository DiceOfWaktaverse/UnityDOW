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
            AddState(new BattleStartState());
            AddState(new BattleUserTurnStartState());
            AddState(new BattleUserDiceRollState());
            AddState(new BattleUserActionState());
            AddState(new BattleUserTurnEndState());
            AddState(new BattleEnemyTurnStartState());
            AddState(new BattleEnemyActionState());
            AddState(new BattleEnemyTurnEndState());
            AddState(new BattleEndState());

            // Set Initial State
            ChangeState<InitialMulliganState>();
        }

        public BattleStateMachine(BattleStage battleStage)
        {
            this.battleStage = battleStage;
        }
    }

    // 유저 인포 초기화 하는 스테이지, 새로하기면 완전 초기화, 이어서 하기면 로드를 함
    public class StartingState : StateBase
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

    // 게임이 처음 시작되고 멀리건을 하는 단계, 만약 유저인포의 progress가 0이면 처음 시작하는 것이고, 0이 아니면 이어서 하는 것이라서 이 스테이트 넘김 
    public class InitialMulliganState : StateBase
    {

        public override bool OnEnter()
        {
            PopupManager.OpenPopup<MulliganPopup>("MulliganPopup");
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

    public class ChapterInfoState : StateBase
    {
        public override bool OnEnter()
        {
            PopupManager.OpenPopup<ChapterMapPopup>("ChapterMapPopup");
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

    public class BattleStartState : BattleState
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