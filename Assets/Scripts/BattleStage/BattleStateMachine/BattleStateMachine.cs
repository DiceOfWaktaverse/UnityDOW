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
        private T CreateState<T>() where T : BattleState, new()//������ ����
        {
            T state = new T();
            state.SetData(data);
            return state;
        }
        public override void SetState()//���� ����
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
            if(!CurState.Update(dt))//���� ���·� �Ѿ�ų� üũ�ϴ� �κе�
            {

            }
            return true;
        }
    }
}