using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleEnemyTurn : BattleState//��� ���͵��� �� ����
    {
        public override bool OnEnter()
        {
            if (base.OnEnter())//������ �ʱ�ȭ, ȿ�� �ִ� �༮�� �߻�
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
            if (base.Update(dt))//������ ������ �ش� ���͵��� �ൿ��� ����.
            {
                return true;
            }
            return false;
        }
    }
}