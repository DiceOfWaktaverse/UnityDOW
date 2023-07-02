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
            // TODO: 지금 테스트하느라 임시로 바로 메인메뉴 가도록 해놓음
            ChangeState<MainMenuState>();
        }
    }

    public class StartSplashState : StateBase
    {
        private StartSplashPopup startSplashPopup = null;

        public override bool OnEnter()
        {
            startSplashPopup = StartSplashPopup.OpenPopup();
            return base.OnEnter();
        }

        public override bool Update(float dt)
        {
            if (Input.anyKeyDown)
            {
                EventManager.TriggerEvent(StartMenuEventType.StartSplashFinished);
                return false;
            }

            return base.Update(dt);
        }

        public override bool OnExit()
        {
            if (startSplashPopup != null)
            {
                startSplashPopup.closeStartSplash();
            }

            return base.OnExit();
        }
    }

    public class MainMenuState : StateBase
    {
        //private MainMenuPopup mainMenuPopup = null;
    
        public override bool OnEnter()
        {
            //mainMenuPopup = MainMenuPopup.OpenPopup();

            return base.OnEnter();
        }

        public override bool OnExit()
        {
            //if (mainMenuPopup != null)
            //{
            //    mainMenuPopup.closeMainMenu();
            //}
            return base.OnExit();
        }
    }

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