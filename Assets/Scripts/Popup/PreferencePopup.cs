using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferencePopupData
    //�ٸ� �����Ͱ� �ʿ��� ��� ��뿹��.
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
        ClosePopup();
    }

}
