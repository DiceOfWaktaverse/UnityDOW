using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleSummon : BattleState //ĳ���� ��ȯ���� ����
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//ĳ���� ī�� ������ �� �ֵ��� �������ֱ�
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
            if (base.Update(dt))//ĳ���� ī�带 �����ؼ� ��ȯ�� �Ǿ����� Ȯ��
            {
                return true;
            }
            return false;
        }
    }
}