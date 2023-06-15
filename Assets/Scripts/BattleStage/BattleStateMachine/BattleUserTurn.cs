using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleUserTurn : BattleState//유저 턴 상태(카드 사용, 상점 이용 등 가능)
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//사용 가능한 카드 세팅하기, 효과 있는 녀석들 발생
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
            if (base.Update(dt))//유저가 턴을 넘김을 확인
            {
                return true;
            }
            return false;
        }
    }
}