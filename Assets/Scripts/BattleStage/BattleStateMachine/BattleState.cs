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
            if (base.OnEnter())//��� ������Ʈ���� �ؾ��ϴ� �ʱ�ȭ
            {
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//��ȯ �Ϸ�� Ŭ����Ǿ�� �ϴ� ����
            {
                return true;
            }
            return false;
        }
        public override bool Update(float dt)//������ ������ �����Ͽ��� �� ���� �����ؾ���
        {
            return base.Update(dt);
        }
    }
}
