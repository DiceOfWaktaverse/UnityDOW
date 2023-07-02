using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{


    public class BattleStateMachine : SimpleStateMachine<StateBase>
    {
        public override void SetState()
        {
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

    }

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
            Debug.Log("ChapterInfoState OnEnter");
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