using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class LoadingSplashState : StateBase, EventListenerBase
    {
        // TODO: this timer is placeholder for now
        // replace to real loading logic in future
        private float timer = 0f;
        private const float loadingTime = 5f;
        private LoadingSplashPopup loadingSplashPopup = null;

        public override bool OnEnter() 
        {
            if (!base.OnEnter()) return false;

            Debug.Log("LoadingSplashState OnEnter");

            loadingSplashPopup = LoadingSplashPopup.OpenPopup();
            Debug.Log("LoadingSplashPopup" + loadingSplashPopup.ToString());


            timer = 0f;
            return true;
        }
    
        public override bool Update(float dt) {
            timer += dt;

            if (timer >= loadingTime) {
                EventManager.TriggerEvent(MainMenuEventType.LoadingSplashFinished);
                return false;
            }

            return base.Update(dt);
        }

        public override bool OnExit()
        {
            if (!base.OnExit()) return false;

            if (loadingSplashPopup != null) loadingSplashPopup.closeLoadingSplash();
            
            Debug.Log("LoadingSplashState OnExit");
            return true;
        }
    }

}
