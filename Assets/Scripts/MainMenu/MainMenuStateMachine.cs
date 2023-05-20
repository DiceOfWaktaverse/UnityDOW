using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class MainMenuStateMachine : SimpleStateMachine<StateBase>
    {
        public override void SetState() {
            AddState(new LoadingSplashState());
            AddState(new StartSplashState());
            AddState(new MainMenuState());

            // Set Initial State
            ChangeState<LoadingSplashState>();
        }
    }
}