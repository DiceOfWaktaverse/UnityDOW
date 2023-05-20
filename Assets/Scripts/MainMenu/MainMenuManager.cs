using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public enum MainMenuEventType
    {
        LoadingSplashFinished,
        StartSplashFinished
    }


    public class MainMenuManager : MonoBehaviour, EventListener<MainMenuEventType>
    {
        private MainMenuStateMachine mainMenuStateMachine = null;
        private GameObject beacon = null;

        public void Awake(){

            PopupManager.Instance.Initialize();
            PopupManager.Instance.SetBeacon(beacon);

            mainMenuStateMachine = new MainMenuStateMachine();
            mainMenuStateMachine.StateInit();
            this.EventStartListening<MainMenuEventType>();
        }

        public void Start()
        {
        }

        public void Update()
        {
            // input deltatime to current state
            if (mainMenuStateMachine.CurState != null){
                mainMenuStateMachine.CurState.Update(Time.deltaTime);
            }
        }

        public void OnEvent(MainMenuEventType eventType) {
            if (eventType == MainMenuEventType.LoadingSplashFinished) {
                mainMenuStateMachine.ChangeState<StartSplashState>();
            }
        }

        public void OnDestroy() {
            this.EventStopListening<MainMenuEventType>();
        }
    }

}