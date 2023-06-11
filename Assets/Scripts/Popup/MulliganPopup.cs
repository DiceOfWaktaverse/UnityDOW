using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class MulliganPopup : Popup<MulliganPopup>
    {
        public static MulliganPopup OpenPopup()
        {
            MulliganPopup popup = PopupManager.OpenPopup<MulliganPopup>("MulliganPopup");
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
