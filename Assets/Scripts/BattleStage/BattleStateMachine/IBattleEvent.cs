using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public interface IBattleEvent
    {
        public int ARRIVE_TURN { get; }
        public SkillCardData CARD_DATA { get; }//ī�嵥���� ������ �� ��ü
        public Object CASTER { get; }//Object�� �ƴ϶� ĳ������ ��ũ��Ʈ�� ��ü
        public List<Object> TARGET { get; }//Object�� �ƴ϶� ĳ������ ��ũ��Ʈ�� ��ü
        public void Set(int cardTag, int value, Object caster, params Object[] target);
        public void SetAction();
    }
}
