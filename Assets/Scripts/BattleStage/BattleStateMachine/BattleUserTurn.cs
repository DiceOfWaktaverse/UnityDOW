using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleUserTurn : BattleState//���� �� ����(ī�� ���, ���� �̿� �� ����)
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//��� ������ ī�� �����ϱ�, ȿ�� �ִ� �༮�� �߻�
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
            if (base.Update(dt))//������ ���� �ѱ��� Ȯ��
            {
                return true;
            }
            return false;
        }
    }
}