using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class DifficultyPopupData //다른 데이터가 필요할 경우 사용예정.
    {
    }
    public class DifficultyPopup : Popup<DifficultyPopupData>
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

        public static DifficultyPopup OpenPopup()
        {
            DifficultyPopup popup = PopupManager.OpenPopup<DifficultyPopup>("DifficultyPopup");
            return popup;
        }

        public virtual void closePreference()
        {
            ClosePopup();
        }
    }
}