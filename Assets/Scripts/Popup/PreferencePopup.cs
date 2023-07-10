using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencePopupData
    //�ٸ� �����Ͱ� �ʿ��� ��� ��뿹��.
{

}

public class PreferencePopup : Popup<PreferencePopupData>
{
    //sound setting
    [SerializeField]
    public Slider masterSlider;
    [SerializeField]
    public Slider bgmSlider;
    [SerializeField]
    public Slider effectSlider;

    private void Awake()
    {
        masterSlider.value = SoundManager.Instance.MasterVolume;
        bgmSlider.value = SoundManager.Instance.BgmVolume;
        effectSlider.value = SoundManager.Instance.EffectVolume;
    }


    public void changeMasterVolume() => SoundManager.Instance.SetMasterVolume(masterSlider.value);
    public void changeBgmVolume() => SoundManager.Instance.SetBgmVolume(bgmSlider.value);
    public void changeEffectVolume() => SoundManager.Instance.SetEffectVolume(effectSlider.value);

    //preference popup
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
