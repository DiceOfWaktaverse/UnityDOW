using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencePopupData
    //다른 데이터가 필요할 경우 사용예정.
{

}

public class PreferencePopup : Popup<PreferencePopupData>
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

    public static PreferencePopup OpenPopup()
    {
        PreferencePopup popup = PopupManager.OpenPopup<PreferencePopup>("PreferencePopup");

        return popup;
    }

    public virtual void closePreference()
    {
        StartMenuPopup.OpenPopup();
        ClosePopup();
    }

}
