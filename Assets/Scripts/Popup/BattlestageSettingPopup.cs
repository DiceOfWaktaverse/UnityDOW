using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattlestageSettingPopup : Popup<BattlestageSettingPopup>
    {
        public static void OpenPopup()
        {
            BattlestageSettingPopup popup = PopupManager.OpenPopup<BattlestageSettingPopup>("BattlestageSettingPopup");
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
