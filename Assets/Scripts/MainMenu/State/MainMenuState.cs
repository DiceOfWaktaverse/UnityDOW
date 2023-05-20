using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class MainMenuState : StateBase
    {
        public override bool OnEnter()
        {
            Debug.Log("MainMenuState OnEnter");
            return base.OnEnter();
        }

        public override bool OnExit()
        {
            Debug.Log("MainMenuState OnExit");
            return base.OnExit();
        }
    }
}