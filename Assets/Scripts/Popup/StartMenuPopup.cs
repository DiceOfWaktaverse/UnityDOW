using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartMenuPopupData //다른 데이터가 필요할 경우 사용예정.
{

}

public class StartMenuPopup : Popup<StartMenuPopupData>
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

    public static StartMenuPopup OpenPopup()
    {
        StartMenuPopup popup = PopupManager.OpenPopup<StartMenuPopup>("StartMenuPopup");

        return popup;
    }

    public virtual void openPreference()
    {
        PreferencePopup.OpenPopup();
        ClosePopup();
    }

}
