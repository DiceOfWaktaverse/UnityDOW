using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleDice : BattleState//�ֻ����� ������ ����
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//�ֻ����� ������ ����
            {
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//�������� ��ȯ�� �ϴ� �� (ex. �ʱ�ȭ ��)
            {
                return true;
            }
            return false;
        }
        public override bool Update(float dt)
        {
            if (base.Update(dt))//�ֻ����� �ٱ������� ����(ĳ���Ϳ� ���)�� �Ǿ��°� Ȯ��
            {
                return true;
            }
            return false;
        }
    }
}