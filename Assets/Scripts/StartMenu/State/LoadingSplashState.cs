using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class LoadingSplashState : StateBase, EventListenerBase
    {
        private VideoScreenPopup videoScreenPopup = null;

        public override bool OnEnter() 
        {
            if (!base.OnEnter()) return false;
            videoScreenPopup = VideoScreenPopup.OpenPopup();

            return true;
        }
    
        public override bool Update(float dt) {
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
