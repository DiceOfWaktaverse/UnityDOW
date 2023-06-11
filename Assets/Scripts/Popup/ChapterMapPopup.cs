using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class ChapterMapPopup : Popup<ChapterMapPopup>
    {
        public static ChapterMapPopup OpenPopup()
        {
            ChapterMapPopup popup = PopupManager.OpenPopup<ChapterMapPopup>("ChapterMapPopup");
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