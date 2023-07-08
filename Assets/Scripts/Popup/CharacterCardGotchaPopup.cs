using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class CharacterCardGotchaPopup : Popup<CharacterCardGotchaPopup>
    {
        public static CharacterCardGotchaPopup OpenPopup()
        {
            CharacterCardGotchaPopup popup = PopupManager.OpenPopup<CharacterCardGotchaPopup>("CharacterCardGotchaPopup");
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