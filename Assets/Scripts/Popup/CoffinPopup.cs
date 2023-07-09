using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class CoffinPopup : Popup<CoffinPopup>
    {
        public static CoffinPopup OpenPopup()
        {
            CoffinPopup popup = PopupManager.OpenPopup<CoffinPopup>("CoffinPopup");
            return popup;
        }

        public override void Initialize()
        {

        }

        public override void InitializeUI()
        {

        }

        public override void Refresh()
        {
            
        }
    }
}

