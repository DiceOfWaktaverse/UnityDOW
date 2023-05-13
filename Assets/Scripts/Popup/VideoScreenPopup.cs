using DOW;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreenPopupData //다른 데이터가 필요할 경우 사용예정.
{
}

public class VideoScreenPopup : Popup<VideoScreenPopupData>
{
    public VideoPlayer VideoPlayer;

    static StartMenu startMenu;
    private bool isFinishVideo = false;

    private void Update()
    {
        //인트로끝나는 지점 체크용
        if (isFinishVideo) return;
        else if ( VideoPlayer.isPaused == true)
        {
            isFinishVideo = true;
            startMenu.finishIntro();
            ClosePopup();
        }
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

    public static VideoScreenPopup OpenPopup(StartMenu _startMneu)
    {
        VideoScreenPopup popup = PopupManager.OpenPopup<VideoScreenPopup>("VideoScreenPopup");

        startMenu = _startMneu;

        return popup;
    }
}
