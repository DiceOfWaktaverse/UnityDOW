using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class StartSplashState : StateBase
    {
        private StartSplashPopup startSplashPopup = null;

        public override bool OnEnter()
        {
            Debug.Log("StartSplashState OnEnter");
            startSplashPopup = StartSplashPopup.OpenPopup();
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            if (Input.anyKeyDown)
            {
                Debug.Log("StartSplashState Update: any key down");
                EventManager.TriggerEvent(StartMenuEventType.StartSplashFinished);
                return false;
            }

            return base.Update(dt);
        }

        public override bool OnExit()
        {
            Debug.Log("StartSplashState OnExit");
            if (startSplashPopup != null)
            {
                startSplashPopup.closeStartSplash();
            }
            return base.OnExit();
        }
    }
}