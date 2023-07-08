using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{

    public enum eVolumeType
    {
        MASTER,
        BGM,
        EFFECT,
    }

    public class SoundSlider : MonoBehaviour
    {
        [SerializeField]
        public Slider slider;

        [SerializeField]
        public eVolumeType volumeType;

        public void Awake()
        {
            // load volume from manager to slider
            switch (volumeType)
            {
                case eVolumeType.MASTER:
                    slider.value = SoundManager.Instance.MasterVolume;
                    break;
                case eVolumeType.BGM:
                    slider.value = SoundManager.Instance.BgmVolume;
                    break;
                case eVolumeType.EFFECT:
                    slider.value = SoundManager.Instance.EffectVolume;
                    break;
            }
        }

        public void changeMasterVolume() => SoundManager.Instance.SetMasterVolume(slider.value);
        public void changeBgmVolume() => SoundManager.Instance.SetBgmVolume(slider.value);
        public void changeEffectVolume() => SoundManager.Instance.SetEffectVolume(slider.value);
    }

}