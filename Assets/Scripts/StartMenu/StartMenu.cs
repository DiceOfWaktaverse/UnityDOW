using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    public VideoPlayer introVideoPlayer;
    public AudioSource startManuAudioSource;
    public GameObject beacon;

    private GameObject startMenuPanel;

    private bool isFinishVideo = false;

    //인트로 실행
    void Start() => Instantiate(introVideoPlayer).Play();

    private void Update()
    {
        //인트로끝나는 지점 체크용
        if (isFinishVideo) return;
        else if ( introVideoPlayer.isPaused == true)
        {
            isFinishVideo = true;
            this.finishIntro();
            GameObject.Destroy(GameObject.Find("VideoScreen"));
        }
    }

    private void finishIntro()
    {
        //인트로가 끝난시점
        //매인 화면이 보여야함
        //매인화면을 enabled하고
        Debug.Log("finishIntro");

        startMenuPanel = GameObject.Find("Canvas").transform.Find("panel_StartMenu").gameObject;
        startMenuPanel.SetActive(true);

        PopupManager.Instance.Initialize();
        PopupManager.Instance.SetBeacon(beacon);
        PopupManager.OpenPopup<PopupBase>("Assets/Scripts/StartMenu/Triangle.prefab");
        //bgm시작
        SoundManager.Instance.Initialize();
        SoundManager.Instance.PushBGM(startManuAudioSource);
    }

    public void openPanel(GameObject panel) => panel.SetActive(true);
    public void closePanel(GameObject panel) => panel.SetActive(false);

}
