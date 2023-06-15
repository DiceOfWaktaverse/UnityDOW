using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleDice : BattleState//주사위를 굴리는 상태
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//주사위를 굴리기 시작
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
            if (base.Update(dt))//주사위가 다굴려저서 선택(캐릭터에 배분)이 되었는가 확인
            {
                return true;
            }
            return false;
        }
    }
}