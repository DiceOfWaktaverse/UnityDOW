using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public class LoadingSplashPopupData
    {

    }
    public class LoadingSplashPopup : Popup<LoadingSplashPopupData>
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

        public static LoadingSplashPopup OpenPopup()
        {
            LoadingSplashPopup popup = PopupManager.OpenPopup<LoadingSplashPopup>("LoadingSplashPopup");
            return popup;
        }

        public virtual void closeLoadingSplash()
        {
            ClosePopup();
        }
    }

}