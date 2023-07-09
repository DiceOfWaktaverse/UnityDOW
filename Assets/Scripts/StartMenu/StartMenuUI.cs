using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DOW {

public class StartMenuUI : UIObject
{
    public override void InitializeUI(eSceneType targetType) {
            if (eSceneType.START_MENU == targetType)
            {
                curSceneType = targetType;
                curUIType = eSceneType.START_MENU;
                ReuseAnim();
            }
            else
            {
                curSceneType = targetType;
                curUIType = eSceneType.NONE;
                UnuseAnim();
            }
        }

        public static void OnClickStart()
        {
            DifficultyPopup.OpenPopup();
        }

        public static void OnClickContinue()
        {
            // TODO: 이어하기구현
        }

        public static void OnClickPreference()
        {
            PreferencePopup.OpenPopup();
        }


        public static void OnClickBook()
        {
            CharacterCardGotchaPopup.OpenPopup();
        }

        public static void OnClickCredit()
        {
            ChooseCardPopup.OpenPopup();
        }

        public static void OnClickExit()
        {
            #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
            #else
            Application.Quit();
            #endif
        }
    
            public override void RefreshUI() {

        }
            public override bool RefreshUI(eSceneType targetType)
        {
            return base.RefreshUI(targetType);
        }
}

}