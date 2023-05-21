using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class MainMenuPopupData
    {
    }

    public class MainMenuPopup : Popup<MainMenuPopupData>
    {
        public override void Initialize()
        {
        }

        public override void InitializeUI()
        {
        }

        public override void Refresh()
        {
        }

        public static MainMenuPopup OpenPopup()
        {
            MainMenuPopup popup = PopupManager.OpenPopup<MainMenuPopup>("MainMenuPopup");

            GameObject btn_Preferences = popup.transform.Find("btn_Preferences").gameObject;
            Button btn_PreferencesComponent = btn_Preferences.GetComponent<Button>();
            btn_PreferencesComponent.onClick.AddListener(() => { openPreferencePopup(); });

            return popup;
        }

        public static void openPreferencePopup() {
            PreferencePopup.OpenPopup();
        }

        public virtual void closeMainMenu()
        {
            ClosePopup();
        }
    }
}