using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class SoundManager : IManagerBase
    {
        private static SoundManager instance = null;
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundManager();

                return instance;
            }
        }
        
        public MonoBehaviour Game { get => DOWGameManager.Instance.GameObject; }

        const float FADE_VALUE = 1f;

        public float MasterVolume { get; protected set; } = 1f;
        public float BgmVolume { get; protected set; } = 1f;
        public float EffectVolume { get; protected set; } = 1f;
        public float BGMVolume { get { return MasterVolume * BgmVolume; } }
        public float SFXVolume { get { return MasterVolume * EffectVolume; } }

        protected Dictionary<string, AudioClip> _clips = null;
        protected List<AudioSource> _playSoundList = null;
        protected List<AudioSource> _loopingSounds = null;
        protected Stack<AudioSource> _playBGMStack = null;
        protected ListPool<AudioSource> _sourcePool = null;
        private int poolCount = 0;

        public void Initialize()
        {
            if (_clips == null)
                _clips = new Dictionary<string, AudioClip>();
            else
                _clips.Clear();

            if (_playSoundList == null)
                _playSoundList = new List<AudioSource>();
            else
                _playSoundList.Clear();

            if (_loopingSounds == null)
                _loopingSounds = new List<AudioSource>();
            else
                _loopingSounds.Clear();

            if (_playBGMStack == null)
                _playBGMStack = new Stack<AudioSource>();
            else
                _playBGMStack.Clear();

            if (_sourcePool == null)
                _sourcePool = new ListPool<AudioSource>(ReuseSource, UnuseSource);

            MasterVolume = PlayerPrefs.GetFloat("master", 1f);
            BgmVolume = PlayerPrefs.GetFloat("bgm", 1f);
            EffectVolume = PlayerPrefs.GetFloat("effect", 1f);

            SpawnPool(10);
        }

		public IEnumerator ClipsLoad()
		{
			yield break;
		}

        public void Update(float dt) {}

        #region SFX
        public void PlaySFX(string _soundName, bool loop = false, float _delay = 0f, Vector3 _location = default)
        {
            if (_soundName is "-1" or "")
                return;
            if (_clips == null)
                return;


            if (_clips.ContainsKey(_soundName) && _clips[_soundName] != null)
            {
                if (_delay <= 0f)
                    PlaySound(_clips[_soundName], _location, loop);
                else
                    Game.StartCoroutine(PlayDelaySound(_clips[_soundName], _delay, _location, loop));
            }
            else
            {
                Debug.LogWarningFormat("Not Loaded Sound => {0}", _soundName);
            }
        }
        public void PlaySFX(AudioClip _clip, bool loop = false, float _delay = 0f, Vector3 _location = default)
        {
            if (_clip == null)
                return;

            if (_delay <= 0.0f)
                PlaySound(_clip, _location, loop);
            else
                Game.StartCoroutine(PlayDelaySound(_clip, _delay, _location, loop));
        }
        public void StopSFX(string _soundName)
        {
            var SfxIT = _playSoundList.GetEnumerator();
            _soundName = Func.StrBuilder("Temp_", _soundName);
            var isStop = false;

            while (SfxIT.MoveNext())
            {
                if (SfxIT.Current == null)
                    continue;

                if (SfxIT.Current.name == _soundName)
                {
                    SfxIT.Current.Stop();
                    isStop = true;
                    break;
                }
            }

            if (isStop)
                return;

            var LoopIT = _loopingSounds.GetEnumerator();

            while (LoopIT.MoveNext())
            {
                if (LoopIT.Current == null)
                    continue;

                if (LoopIT.Current.name == _soundName)
                {
                    StopLoopingSound(LoopIT.Current);
                    break;
                }
            }
        }
        public void StopLoopingSound(AudioSource source)
        {
            if (source != null)
            {
                source.Stop();
                _loopingSounds.Remove(source);
                _sourcePool.Put(source);
            }
        }
        protected IEnumerator PlayDelaySound(AudioClip _clip, float _delay, Vector3 _location, bool _loop)
        {
            yield return new WaitForSeconds(_delay);

            PlaySound(_clip, _location, _loop);
        }
        protected IEnumerator TrackingSoundCO(AudioSource source, float delay, bool isDelete)
        {
            if (source == null)
                yield break;

            _playSoundList.Add(source);

            yield return new WaitForSeconds(delay);

            _playSoundList.Remove(source);
            if (isDelete && source != null)
                _sourcePool.Put(source);
        }
        public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, bool loop = false, float power = 1f)
        {
            if (!loop && (sfx == null || SFXVolume <= 0f))
                return null;
            AudioSource audioSource = GetSource();
            audioSource.clip = sfx;
            audioSource.volume = SFXVolume;
            audioSource.loop = loop;
            audioSource.Play();

            if (!loop)
                Game.StartCoroutine(TrackingSoundCO(audioSource, sfx.length, true));
            else
                _loopingSounds.Add(audioSource);

            return audioSource;
        }

        public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, float pitch, float pan, float spatialBlend = 0.0f, bool loop = false,
            AudioSource reuseSource = null, UnityEngine.Audio.AudioMixerGroup audioGroup = null)
        {
            if (!loop && (sfx == null || SFXVolume <= 0f))
                return null;

            if (reuseSource == null)
            {
                var newAudioSource = GetSource();
                reuseSource = newAudioSource;
                reuseSource.transform.parent = Game.transform;
                reuseSource.transform.position = location;
            }

            reuseSource.time = 0.0f;
            reuseSource.clip = sfx;
            reuseSource.pitch = pitch;
            reuseSource.spatialBlend = spatialBlend;
            reuseSource.panStereo = pan;

            reuseSource.volume = SFXVolume;
            reuseSource.loop = loop;
            if (audioGroup != null)
                reuseSource.outputAudioMixerGroup = audioGroup;

            reuseSource.Play();

            if (!loop)
                Game.StartCoroutine(TrackingSoundCO(reuseSource, sfx.length, reuseSource == null));
            else
                _loopingSounds.Add(reuseSource);

            return reuseSource;
        }
        public void AllStopSound()
        {
            if (_playSoundList != null)
            {
                while (_playSoundList.Count > 0)
                {
                    if (_playSoundList[0] == null)
                    {
                        _playSoundList.RemoveAt(0);
                        continue;
                    }

                    _playSoundList[0].Stop();
                    _playSoundList.RemoveAt(0);
                }
            }

            if (_loopingSounds != null)
            {
                while (_loopingSounds.Count > 0)
                {
                    if (_loopingSounds[0] == null)
                    {
                        _loopingSounds.RemoveAt(0);
                        continue;
                    }

                    StopLoopingSound(_loopingSounds[0]);
                }
            }

            if (_playBGMStack != null)
            {
                if (_playBGMStack.Count > 0)
                    _playBGMStack.Peek().Stop();

                _playBGMStack.Clear();
            }
        }
        #endregion
        #region BGM
        public virtual AudioSource PlayBGM()
        {
            if (_playBGMStack == null || _playBGMStack.Count < 1)
                return null;

            var target = _playBGMStack.Peek();
            if(target != null)
            {
                target.volume = BGMVolume;
                target.Play();
            }

            return target;
        }
        public virtual AudioSource StopBGM()
        {
            if (_playBGMStack == null || _playBGMStack.Count < 1)
                return null;

            var target = _playBGMStack.Peek();
            if (target != null)
            {
                target.volume = BGMVolume;
                target.Stop();
            }

            return target;
        }
        public virtual void PushBGM(AudioSource Music, bool isLoop = true)
        {
            if (_playBGMStack == null || Music == null)
                return;

            AudioSource prev = null;
            if (_playBGMStack != null && _playBGMStack.Count > 0)
                prev = _playBGMStack.Pop();

            Music.volume = 0f;
            Music.loop = isLoop;

            _playBGMStack.Push(Music);

            BGMSoundChange(prev, Music);
        }

        public virtual void PushBGM(string soundName, bool isLoop = true)
        {
            if (_playBGMStack == null)
                return;

            AudioSource currentAudio = GetSource();
            if (_clips.ContainsKey(soundName))
                currentAudio.clip = _clips[soundName];

            currentAudio.playOnAwake = false;

            AudioSource prev = null;
            if (_playBGMStack != null && _playBGMStack.Count > 0)
                prev = _playBGMStack.Pop();

            currentAudio.volume = 0f;
            currentAudio.loop = isLoop;

            _playBGMStack.Push(currentAudio);

            BGMSoundChange(prev, currentAudio);
        }

        public virtual void PopBGM()
        {
            AudioSource prev = null;
            if (_playBGMStack.Count > 0)
                prev = _playBGMStack.Pop();

            if (_playBGMStack == null || _playBGMStack.Count == 0)
            {
                BGMSoundChange(prev, null);
                return;
            }

            var target = _playBGMStack.Peek();
            if (target == null)
            {
                BGMSoundChange(prev, null);
                return;
            }

            BGMSoundChange(prev, target);
        }
        #endregion
        #region Volume
        protected IEnumerator curSoundInCO = null;
        protected AudioSource nextSource = null;
        void BGMSoundChange(AudioSource prev, AudioSource next)
        {
            if (prev != null)
            {
                Game.StartCoroutine(SoundOut(prev));
            }

            if (next != null)
            {
                if (curSoundInCO != null)
                {
                    Game.StopCoroutine(curSoundInCO);
                    if (nextSource != null && nextSource.volume > 0f)
                    {
                        Game.StartCoroutine(SoundOut(nextSource));
                        nextSource = null;
                    }
                    curSoundInCO = null;
                }

                curSoundInCO = SoundIn(next);
                Game.StartCoroutine(curSoundInCO);
            }
        }
        IEnumerator SoundIn(AudioSource inSound)
        {
            if (inSound != null)
            {
                nextSource = inSound;
                inSound.volume = 0f;
                inSound.Play();
                while (inSound.volume < BGMVolume)
                {
                    yield return null;
                    inSound.volume += Time.deltaTime * FADE_VALUE;

                    if (inSound == null)
                        yield break;
                }
                inSound.volume = BGMVolume;
            }
            curSoundInCO = null;
        }
        IEnumerator SoundOut(AudioSource outSound)
        {
            if (outSound != null)
            {
                while (outSound.volume > 0f)
                {
                    yield return null;

                    if (outSound == null)
                        yield break;

                    outSound.volume -= Time.deltaTime * FADE_VALUE;
                }
                outSound.volume = 0f;
                outSound.Stop();

                _sourcePool.Put(outSound);
            }
        }
        public void SetMasterVolume(float _volume)
        {
            MasterVolume = _volume;
            PlayerPrefs.SetFloat("masterVolume", MasterVolume);
            BackgroundVolumeControl();
            EffectVolumeControl();
        }
        public void SetBgmVolume(float _volume)
        {
            BgmVolume = _volume;
            PlayerPrefs.SetFloat("bgmVolume", BgmVolume);
            BackgroundVolumeControl();
        }
        public void SetEffectVolume(float _volume)
        {
            EffectVolume = _volume;
            PlayerPrefs.SetFloat("effectVolume", EffectVolume);
            EffectVolumeControl();
        }
        public void VolumeControl(float mVolume, float bVolume, float eVolume)
        {
            MasterVolume = mVolume;
            BgmVolume = bVolume;
            EffectVolume = eVolume;
            BackgroundVolumeControl();
            EffectVolumeControl();
        }

        public void BackgroundVolumeControl()
        {
            if(_playBGMStack != null && _playBGMStack.Count > 0)
            {
                var bgm = _playBGMStack.Peek();
                if (bgm != null && bgm != null)
                    bgm.volume = BGMVolume;
            }
        }

        public void EffectVolumeControl()
        {
            if (_loopingSounds != null && _loopingSounds.Count > 0)
            {
                for (int i = 0; i < _loopingSounds.Count; i++)
                {
                    if (_loopingSounds[i] == null)
                        continue;

                    _loopingSounds[i].volume = SFXVolume;
                }
            }

            if (_playSoundList != null && _playSoundList.Count > 0)
            {
                for (int i = 0; i < _playSoundList.Count; i++)
                {
                    if (_playSoundList[i] == null)
                        continue;

                    _playSoundList[i].volume = SFXVolume;
                }
            }
        }
        #endregion
        #region SourcePool
        private AudioSource GetSource()
        {
            if (_sourcePool.Count < 1)
                SpawnPool(1);

            return _sourcePool.Get();
        }
        private AudioSource GetSource(AudioSource data)
        {
            var source = GetSource();

            source.clip = data.clip;
            source.time = 0.0f;
            source.volume = data.volume;
            source.playOnAwake = data.playOnAwake;
            source.loop = data.loop;
            source.pitch = data.pitch;
            source.spatialBlend = data.spatialBlend;
            source.panStereo = data.panStereo;
            source.outputAudioMixerGroup = data.outputAudioMixerGroup;

            return source;
        }
        private void SpawnPool(int count)
        {
            if (_sourcePool == null)
                return;

            while (_sourcePool.Count < count)
            {
                GameObject soundObject = new GameObject(Func.StrBuilder("Sound_", poolCount));
                soundObject.transform.parent = Game.transform;
                soundObject.transform.position = default;
                _sourcePool.Put(soundObject.AddComponent<AudioSource>());
                poolCount++;
            }
        }
        private void ReuseSource(AudioSource source)
        {
            if (source == null)
                return;

            source.gameObject.SetActive(true);
        }
        private void UnuseSource(AudioSource source)
        {
            if (source == null)
                return;

            source.Stop();
            source.clip = null;
            source.time = 0.0f;
            source.volume = 0f;
            source.playOnAwake = false;
            source.loop = false;
            source.pitch = 1f;
            source.spatialBlend = 0f;
            source.panStereo = 0f;
            source.outputAudioMixerGroup = null;

            source.gameObject.SetActive(false);
        }
        #endregion
    }
}