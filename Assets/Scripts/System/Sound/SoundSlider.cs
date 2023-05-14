using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Slider slider;
    public void changeMasterVolume() => SoundManager.Instance.SetMasterVolume(slider.value);
    public void changeBgmVolume() => SoundManager.Instance.SetBgmVolume(slider.value);
    public void changeEffectVolume() => SoundManager.Instance.SetEffectVolume(slider.value);
}
