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
        private VideoScreenPopup videoScreenPopup = null;

        public override bool OnEnter() 
        {
            if (!base.OnEnter()) return false;
            videoScreenPopup = VideoScreenPopup.OpenPopup();

            timer = 0f;
            return true;
        }
    
        public override bool Update(float dt) {
            timer += dt;

            // if video is finished, close popup
            if (videoScreenPopup != null && videoScreenPopup.VideoPlayer.isPaused == true) {
                EventManager.TriggerEvent(StartMenuEventType.LoadingSplashFinished);
                return false;
            }

            return base.Update(dt);
        }

        public override bool OnExit()
        {
            if (!base.OnExit()) return false;

            if (videoScreenPopup != null) {
                videoScreenPopup.closeVideoScreen();
            }
            
            return true;
        }
    }

}
