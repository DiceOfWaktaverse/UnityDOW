using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattleStage : MonoBehaviour
    {
        private BattleStateMachine stateMachine = new BattleStateMachine();

        void Start()
        {
            stateMachine.StateInit();
        }

        void Update()
        {
            if (stateMachine.CurState != null)
                stateMachine.CurState.Update(Time.deltaTime);
        }
    }
}