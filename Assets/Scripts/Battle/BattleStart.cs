using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleStart : BattleState
    {
        protected readonly float Delay = 3f;//Delay �� �ִٸ�
        protected float delay = 3f;
        public override bool OnEnter()
        {
            if (base.OnEnter())//���� ù ���۽� �ؾ��� �ϵ�
            {
                delay = Delay;
                return true;
            }
            return false;
        }
        public override bool OnExit()
        {
            if (base.OnExit())//�������� ��ȯ�� �ϴ� ��
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