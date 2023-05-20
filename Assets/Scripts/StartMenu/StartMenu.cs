using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    public AudioSource startManuAudioSource;
    public GameObject beacon;

    //��Ʈ�� ����
    void Start()
    {
        PopupManager.Instance.Initialize();
        PopupManager.Instance.SetBeacon(beacon);


        // VideoScreenPopup.OpenPopup(this);
        LoadingSplashPopup.OpenPopup();
    }

    public void finishIntro()
    {
        //��Ʈ�ΰ� ��������
        //���� ȭ���� ��������
        //����ȭ���� enabled�ϰ�
        Debug.Log("finishIntro");

        //bgm����
        SoundManager.Instance.Initialize();
        SoundManager.Instance.PushBGM(startManuAudioSource);
    }

    public static void openPreferencePopup() => PreferencePopup.OpenPopup();

}
