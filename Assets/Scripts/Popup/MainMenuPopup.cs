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
            return popup;
        }

        public static void openDifficultyPopup() {
            DifficultyPopup.OpenPopup();
        }

        public static void openPreferencePopup() {
            PreferencePopup.OpenPopup();
        }

        public static void openTEST_ShopPopup() { 
            ShopPopup.OpenPopup();
        }

        public virtual void closeMainMenu()
        {
            ClosePopup();
        }
    }
}