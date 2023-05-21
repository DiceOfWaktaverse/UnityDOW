using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class StartMenuStateMachine : SimpleStateMachine<StateBase>
    {
        public override void SetState() {
            // Loading splash -> intro video
            AddState(new LoadingSplashState());
            // Start splash -> press any key to continue
            AddState(new StartSplashState());
            // Main menu -> start game, options, exit
            AddState(new MainMenuState());

            // Set Initial State
            ChangeState<LoadingSplashState>();
        }
    }
}