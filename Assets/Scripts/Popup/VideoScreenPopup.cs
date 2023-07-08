using DOW;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreenPopupData 
{
}

public class VideoScreenPopup : Popup<VideoScreenPopupData>
{
    public VideoPlayer VideoPlayer;

    public override void Initialize()
    {

    }

    public override void InitializeUI()
    {

    }

    public override void Refresh()
    {

    }

    public static VideoScreenPopup OpenPopup()
    {
        VideoScreenPopup popup = PopupManager.OpenPopup<VideoScreenPopup>("VideoScreenPopup");
        return popup;
    }

    public virtual void closeVideoScreen()
    {
        ClosePopup();
    }
}
