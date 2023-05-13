using DOW;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class VideoScreenPopupData //�ٸ� �����Ͱ� �ʿ��� ��� ��뿹��.
{
}

public class VideoScreenPopup : Popup<VideoScreenPopupData>
{
    public VideoPlayer VideoPlayer;

    static StartMenu startMenu;
    private bool isFinishVideo = false;

    private void Update()
    {
        //��Ʈ�γ����� ���� üũ��
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
