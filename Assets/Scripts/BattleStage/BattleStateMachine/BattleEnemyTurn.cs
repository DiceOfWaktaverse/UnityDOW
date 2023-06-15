using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleEnemyTurn : BattleState//상대 몬스터들의 턴 상태
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//데이터 초기화, 효과 있는 녀석들 발생
            {
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//다음상태 전환시 하는 일 (ex. 초기화 등)
            {
                return true;
            }
            return false;
        }
        public override bool Update(float dt)
        {
            if (base.Update(dt))//랜덤을 돌려서 해당 몬스터들의 행동방식 구성.
            {
                return true;
            }
            return false;
        }
    }
}