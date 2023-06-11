using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleStateMachine : SimpleStateMachine<BattleState>
    {
        private BattleInfo data = null;
        public void SetData(BattleInfo data)
        {
            this.data = data;
        }
        private T CreateState<T>() where T : BattleState, new()//생성부 통일
        {
            T state = new T();
            state.SetData(data);
            return state;
        }
        public override void SetState()//상태 세팅
        {
            AddState(CreateState<BattleStart>());
            AddState(CreateState<BattleSummon>());
            AddState(CreateState<BattleDice>());
            AddState(CreateState<BattleUserTurn>());
            AddState(CreateState<BattleEnemyTurn>());
            AddState(CreateState<BattleEnd>());

            ChangeState<BattleStart>();
        }
        public bool Update(float dt)
        {
            if(!CurState.Update(dt))//다음 상태로 넘어가거나 체크하는 부분들
            {

            }
            return true;
        }
    }
}