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

    //��Ʈ�� ����
    void Start() => Instantiate(introVideoPlayer).Play();

    private void Update()
    {
        //��Ʈ�γ����� ���� üũ��
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
        //��Ʈ�ΰ� ��������
        //���� ȭ���� ��������
        //����ȭ���� enabled�ϰ�
        Debug.Log("finishIntro");

        startMenuPanel = GameObject.Find("Canvas").transform.Find("panel_StartMenu").gameObject;
        startMenuPanel.SetActive(true);

        PopupManager.Instance.Initialize();
        PopupManager.Instance.SetBeacon(beacon);
        PopupManager.OpenPopup<PopupBase>("Assets/Scripts/StartMenu/Triangle.prefab");
        //bgm����
        SoundManager.Instance.Initialize();
        SoundManager.Instance.PushBGM(startManuAudioSource);
    }

    public void openPanel(GameObject panel) => panel.SetActive(true);
    public void closePanel(GameObject panel) => panel.SetActive(false);

}
