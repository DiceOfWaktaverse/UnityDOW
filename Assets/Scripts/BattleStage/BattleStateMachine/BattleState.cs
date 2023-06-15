using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleState : StateBase
    {
        protected BattleInfo Data { get; set; } = null;
        protected List<IBattleEvent> events = null;
        public void SetData(BattleInfo data)
        {
            Data = data;
        }
        public override bool OnEnter()
        {
            if (base.OnEnter())//모든 스테이트에서 해야하는 초기화
            {
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//전환 완료시 클리어되어야 하는 내용
            {
                return true;
            }
            return false;
        }
        public override bool Update(float dt)//유저나 적군이 전멸하였을 때 게임 종료해야함
        {
            return base.Update(dt);
        }
    }
}
