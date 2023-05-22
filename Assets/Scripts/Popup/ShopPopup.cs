using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class ShopPopup : Popup<ShopPopup>
    {
        public static ShopPopup OpenPopup()
        {
            ShopPopup popup = PopupManager.OpenPopup<ShopPopup>("ShopPopup");
            return popup;
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeUI()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
