using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleEnd : BattleState//전투 종료 시 일어나는 일들에 대한 상태
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//데이터 초기화
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
            if (base.Update(dt))//끝난 후 딜레이 or 보상? 선택 등 기다림
            {
                return true;
            }
            return false;
        }
    }
}