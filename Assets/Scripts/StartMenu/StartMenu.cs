using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace DOW
{
    public enum StartMenuEventType
    {
        LoadingSplashFinished,
        StartSplashFinished,
        DifficultySelected,
    }
    public class StartMenu : MonoBehaviour, EventListener<StartMenuEventType>
    {
        public AudioSource startMenuAudioSource;
        public GameObject beacon;
        public static StartMenuStateMachine startMenuStateMachine = new StartMenuStateMachine();

        void Start()
        {
            PopupManager.Instance.Initialize();
            PopupManager.Instance.SetBeacon(beacon);
            SoundManager.Instance.Initialize();
            
            startMenuStateMachine.StateInit();
            this.EventStartListening<StartMenuEventType>();
        }

        public void Update() {
            // input deltatime to current state
            if (startMenuStateMachine.CurState != null) {
                startMenuStateMachine.CurState.Update(Time.deltaTime);
            }
        }

        public void OnEvent(StartMenuEventType eventType) {
            if (eventType == StartMenuEventType.LoadingSplashFinished) {
                startMenuStateMachine.ChangeState<StartSplashState>();
                SoundManager.Instance.PushBGM(startMenuAudioSource);
            }
            if (eventType == StartMenuEventType.StartSplashFinished) {
                startMenuStateMachine.ChangeState<MainMenuState>();
            }
            if (eventType == StartMenuEventType.DifficultySelected) {
                Debug.Log("Go to Next Scene");
            }
        }

        public void OnDestroy() {
            this.EventStopListening<StartMenuEventType>();
        }
    }
}