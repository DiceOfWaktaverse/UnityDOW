using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    public AudioSource startManuAudioSource;
    public GameObject beacon;

    //인트로 실행
    void Start()
    {
        PopupManager.Instance.Initialize();
        PopupManager.Instance.SetBeacon(beacon);


        VideoScreenPopup.OpenPopup(this);
    }

    public void finishIntro()
    {
        //인트로가 끝난시점
        //매인 화면이 보여야함
        //매인화면을 enabled하고
        Debug.Log("finishIntro");

        //bgm시작
        SoundManager.Instance.Initialize();
        SoundManager.Instance.PushBGM(startManuAudioSource);
    }

    public static void openPreferencePopup() => PreferencePopup.OpenPopup();

}
