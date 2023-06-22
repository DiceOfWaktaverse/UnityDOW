using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleStart : BattleState
    {
        protected readonly float Delay = 3f;//Delay 가 있다면
        protected float delay = 3f;
        public override bool OnEnter()
        {
            if (base.OnEnter())//전투 첫 시작시 해야할 일들
            {
                delay = Delay;
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//다음상태 전환시 하는 일
            {
                delay = Delay;
                return true;
            }
            return false;
        }

        public override bool Update(float dt)
        {
            if (base.Update(dt))
            {
                delay -= dt;
                return delay > 0f;
            }
            return false;
        }
    }
}