using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public interface IBattleEvent
    {
        public int ARRIVE_TURN { get; }
        public SkillCardData CARD_DATA { get; }//카드데이터 정리된 후 교체
        public Object CASTER { get; }//Object가 아니라 캐스터의 스크립트로 교체
        public List<Object> TARGET { get; }//Object가 아니라 캐스터의 스크립트로 교체
        public void Set(int cardTag, int value, Object caster, params Object[] target);
        public void SetAction();
    }
}
