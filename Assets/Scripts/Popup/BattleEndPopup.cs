using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DOW
{
    public class BattleEndPopup : Popup<BattleEndPopup>
    {
        public static void OpenPopup()
        {
            PopupManager.OpenPopup<BattleEndPopup>("BattleEndPopup");
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
