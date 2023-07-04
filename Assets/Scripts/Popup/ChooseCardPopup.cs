using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardPopup : Popup<ChooseCardPopup>
{

    public ChooseCardPopup OpenPopup()
    {
        ChooseCardPopup popup = PopupManager.OpenPopup<ChooseCardPopup>("ChooseCardPopup");
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
