using UnityEngine;

namespace DOW
{
    public struct VolumeData
    {
        public float volume;
        public bool isOn;

        public void Init()
        {
            volume = 1f;
            isOn = true;
        }
    }

    public class GamePreferenceData
    {
        //방송에서 언어 설정은 필요없지만 간단하게 구글번역기 정도로 하면 공수 적고 괜찮아보임.
        private SystemLanguage gameLanguage = (SystemLanguage)PlayerPrefs.GetInt("Setting_Language", (int)Application.systemLanguage);
        public SystemLanguage GameLanguage
        {
            get
            {
                switch (gameLanguage)
                {
                    case SystemLanguage.Japanese:
                    case SystemLanguage.English:
                    case SystemLanguage.Korean:
                        return gameLanguage;
                    default:
                        PlayerPrefs.SetInt("Setting_Language", (int)SystemLanguage.Korean);
                        gameLanguage = SystemLanguage.Korean;
                        return SystemLanguage.Korean;
                }
            }

            set
            {
                PlayerPrefs.SetInt("Setting_Language", (int)value);
                gameLanguage = value;
            }
        }

        VolumeData BGMData = new VolumeData();
        VolumeData SFXData = new VolumeData();
        public GamePreferenceData()
        {
            InitGamePrefData();
        }

        #region Volume Setting
        public VolumeData GetBGMData()
        {
            return BGMData;
        }
        public VolumeData GetSFXData()
        {
            return SFXData;
        }

        public void InitGamePrefData() //설정을 위한 볼륨 및 효과음 저장은 로컬로.
        {
            SetBgm();
            SetSfx();
        }

        void SetBgm()
        {
            BGMData.Init();
            BGMData.volume = PlayerPrefs.GetFloat("Setting_BGM_Volume", 1f);
            BGMData.isOn = PlayerPrefs.GetFloat("Setting_BGM_On", 1) == 1 ? true : false;
            SetBgmVolume();
        }

        public void SetBgm(float _volume, bool _isOn)
        {
            BGMData.volume = _volume;
            BGMData.isOn = _isOn;
            SetBgmVolume();
        }

        public void SetSfx(float _volume, bool _isOn)
        {
            SFXData.volume = _volume;
            SFXData.isOn = _isOn;
            SetSfxVolume();
        }

        void SetSfx()
        {
            SFXData.Init();
            SFXData.volume = PlayerPrefs.GetFloat("Setting_SFX_Volume", 1f);
            SFXData.isOn = PlayerPrefs.GetFloat("Setting_SFX_On", 1) == 1 ? true : false;
            SetSfxVolume();
        }

        void SetBgmVolume()
        {
            SoundManager.Instance.SetBgmVolume(GetBgmVolume());
        }

        public float GetBgmVolume()
        {
            return BGMData.isOn? BGMData.volume: 0f;
        }

        void SetSfxVolume()
        {
            SoundManager.Instance.SetEffectVolume(GetSfxVolume());
        }
        public float GetSfxVolume()
        {
            return SFXData.isOn ? SFXData.volume : 0f;
        }
        #endregion
    }
}
