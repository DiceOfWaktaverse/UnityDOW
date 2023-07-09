using UnityEditor;
using UnityEngine;

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
        [SerializeField]
        protected AudioClip bgmClip = null;
        public static StartMenuStateMachine startMenuStateMachine = new StartMenuStateMachine();


        void Awake()
        {
            UIManager.Instance.InitUI(eSceneType.START_MENU);
            startMenuStateMachine.StateInit();
            this.EventStartListening();
        }

        void OnDestroy()
        {
            this.EventStopListening();
        }

        public void Update()
        {
            // input deltatime to current state
            if (startMenuStateMachine.CurState != null)
            {
                startMenuStateMachine.CurState.Update(Time.deltaTime);
            }
        }

        public void OnEvent(StartMenuEventType eventType)
        {
            if (eventType == StartMenuEventType.LoadingSplashFinished)
            {
                startMenuStateMachine.ChangeState<MainMenuState>();
                SoundManager.Instance.PushBGM(bgmClip);
            }
            // if (eventType == StartMenuEventType.StartSplashFinished) {
            //     startMenuStateMachine.ChangeState<MainMenuState>();
            // }
            if (eventType == StartMenuEventType.DifficultySelected)
            {
                SoundManager.Instance.PopBGM();
                LoadingManager.Instance.LoadScene("BattleStage");
            }
        }
    }
}