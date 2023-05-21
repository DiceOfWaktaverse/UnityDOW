using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public class StartSplashPopupData
    {

    }

    public class StartSplashPopup : Popup<StartSplashPopupData>
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

        public static StartSplashPopup OpenPopup()
        {
            StartSplashPopup popup = PopupManager.OpenPopup<StartSplashPopup>("StartSplashPopup");
            return popup;
        }

        public virtual void closeStartSplash()
        {
            ClosePopup();
        }
    }
}

