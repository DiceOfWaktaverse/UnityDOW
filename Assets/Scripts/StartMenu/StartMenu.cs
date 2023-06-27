using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField]
        protected AudioClip bgmClip = null;
        public static StartMenuStateMachine startMenuStateMachine = new StartMenuStateMachine();


        void Start()
        {
            UIManager.Instance.InitUI(eSceneType.MAIN_MENU);
            startMenuStateMachine.StateInit();
            this.EventStartListening();

            string a = TableManager.GetTable<CardTable>().ToString();
            Debug.Log(a);
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

        public static void OnClickDifficulty()
        {
            DifficultyPopup.OpenPopup();
        }

        public static void OnClickPreference()
        {
            PreferencePopup.OpenPopup();
        }

        public static void OnClickShop()
        {
            MulliganPopup.OpenPopup();
        }

        public static void OnClickBook()
        {
            CharacterCardGotchaPopup.OpenPopup();
        }

        public static void OnClickCredit()
        {
            SystemPopup.OpenPopup("����", "���� ũ������ �������� �ʾҽ��ϴ�.", "Ȯ��");
        }

        public static void OnClickExit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}