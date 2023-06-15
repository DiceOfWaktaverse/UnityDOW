using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleSummon : BattleState //캐릭터 소환시점 상태
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//캐릭터 카드 선택할 수 있도록 세팅해주기
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
            if (base.Update(dt))//캐릭터 카드를 선택해서 소환이 되었는지 확인
            {
                return true;
            }
            return false;
        }
    }
}