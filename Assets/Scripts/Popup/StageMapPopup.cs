using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class StageMapPopup : Popup<StageMapPopup>
    {
        public static StageMapPopup OpenPopup()
        {
            StageMapPopup popup = PopupManager.OpenPopup<StageMapPopup>("StageMapPopup");
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