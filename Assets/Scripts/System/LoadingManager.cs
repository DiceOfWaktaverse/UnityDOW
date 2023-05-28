using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DOW
{
    public enum eLoadingEffectType
    {
        IMMEDIATE,
        FADE_LOGO,
        MAX
    }
    public class LoadingManager : Singleton<LoadingManager>
    {
        private CanvasGroup CanvasGroup { get; set; } = null;
        private Dictionary<eLoadingEffectType, GameObject> Background { get; set; } = null;
        private float blinkTimer = 0.5f;

        private MonoBehaviour Object { get => DOWGameManager.Instance.GameObject; }

        private VoidDelegate sceneCallback;
        private string loadSceneName = "";
        public string CurrentScene { get => loadSceneName; }

        private Stack<string> SceneStack = new Stack<string>(); //씬 뒤로 가기 적용을 위한 스텍
        private bool isLoadingEnd = false;
        public bool IsStartLoadDone { get; private set; }
        public bool IsLoadingEnd { get { return isLoadingEnd && IsStartLoadDone; } }
        private IEnumerator coroutine = null;

        public override void Initialize()
        {
            if (CanvasGroup == null)
                CanvasGroup = new();
            if (Background == null)
                Background = new();
        }

        public void SetBeacon(CanvasGroup value, List<SceneFadeData> param)
        {
            CanvasGroup = value;
            if (param == null)
                return;

            for(int i = 0, count = param.Count; i < count; ++i)
            {
                if (param[i].Target == null)
                    continue;

                Background.TryAdd(param[i].Type, param[i].Target);
            }
        }

        /// <summary>
        /// 다음 씬 로드
        /// </summary>
        /// <param name="sceneName">씬 이름</param>
        /// <param name="eEffect">이펙트 타입</param>
        /// <param name="loadingCompleateCO">이펙트 이후 실행해야할 코루틴 리스트</param>
        public void LoadScene(string sceneName, eLoadingEffectType eEffect = eLoadingEffectType.FADE_LOGO, params IEnumerator[] loadingCompleateCO)
        {
            InitState();
            loadSceneName = sceneName;
            SceneManager.sceneLoaded += LoadSceneEnd;

            coroutine = FadeCoroutine(eEffect, blinkTimer, blinkTimer, null, MoveScene(sceneName, coroutine));
            Object.StartCoroutine(coroutine);
        }

        void InitState()
        {
            if (PopupManager.OpenPopupCount > 0)
                PopupManager.AllClosePopup();

            isLoadingEnd = false;
            if (coroutine != null)
            {
                Object.StopCoroutine(coroutine);
                coroutine = null;
            }
            CanvasGroup.gameObject.SetActive(false);
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }

        private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == loadSceneName)
            {
                isLoadingEnd = true;
                sceneCallback?.Invoke();
                sceneCallback = null;

                if (SceneStack.Contains(loadSceneName))
                {
                    for (int i = 0, count = SceneStack.Count; i < count; ++i)
                    {
                        var popVal = SceneStack.Pop();
                        if (popVal == loadSceneName)
                            break;
                    }
                }
                else
                {
                    SceneStack.Push(loadSceneName);
                }
            }
        }

        private IEnumerator MoveScene(string sceneName, params IEnumerator[] coroutines)
        {
            SceneManager.LoadScene(sceneName);

            if (coroutines != null)
            {
                for (int i = 0, count = coroutines.Length; i < count; ++i)
                {
                    var coroutine = coroutines[i];
                    if (coroutine == null)
                        continue;
                    yield return coroutine;
                }
            }

            yield return new WaitUntil(() => isLoadingEnd);
        }

        private IEnumerator FadeCoroutine(eLoadingEffectType type, float fadeInTime, float fadeOutTime, VoidDelegate cb = null, IEnumerator coroutine = null)
        {
            yield return FadeInCoroutine(type, fadeInTime);

            cb?.Invoke();
            if (coroutine != null)
                yield return coroutine;

            yield return FadeOutCoroutine(type, fadeOutTime);
        }

        private IEnumerator FadeInCoroutine(eLoadingEffectType type, float fadeInTime)
        {
            float timer = 0f;

            CanvasGroup.gameObject.SetActive(true);
            CanvasGroup.alpha = 0.0f;

            var it = Background.GetEnumerator();
            while(it.MoveNext())
            {
                if (it.Current.Value == null)
                    continue;

                it.Current.Value.SetActive(type == it.Current.Key);
            }

            //하얀색 빠르게
            while (timer < fadeInTime)
            {
                yield return null;
                timer += Time.deltaTime;
                CanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeInTime);
            }

            CanvasGroup.alpha = 1.0f;
        }

        private IEnumerator FadeOutCoroutine(eLoadingEffectType type, float fadeOutTime)
        {
            float timer = 0f;
            CanvasGroup.alpha = 0.0f;

            while (timer < fadeOutTime)
            {
                yield return null;
                timer += Time.deltaTime;
                CanvasGroup.alpha = Mathf.Lerp(1, 0, timer / fadeOutTime);
            }

            CanvasGroup.alpha = 0.0f;

            var it = Background.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value == null)
                    continue;

                it.Current.Value.SetActive(false);
            }
            CanvasGroup.gameObject.SetActive(false);
        }
    }
}