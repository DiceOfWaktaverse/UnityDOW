using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardPopup : Popup<ChooseCardPopup>
{
    static ChooseCard chooseCard;

    public static ChooseCardPopup OpenPopup()
    {
        ChooseCardPopup popup = PopupManager.OpenPopup<ChooseCardPopup>("ChooseCardPopup");
        chooseCard = popup.GetComponent<ChooseCard>();
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
