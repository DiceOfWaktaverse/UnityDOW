using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public abstract class UIObject : MonoBehaviour, IUIBase
    {
        protected eSceneType curSceneType = eSceneType.NONE;
        [SerializeField]
        protected eSceneType curUIType = eSceneType.NONE;
        [SerializeField]
        protected List<UIObject> uiChildrens = null;

        public virtual void Initialize()
        {
            if (uiChildrens == null || uiChildrens.Count < 1)
                return;

            var count = uiChildrens.Count;
            for (var i = 0; i < count; ++i)
            {
                if (uiChildrens[i] == null || uiChildrens[i] == this)
                    continue;

                uiChildrens[i].Initialize();
            }
        }
        public virtual void InitializeUI(eSceneType targetType)
        {
            if (curSceneType != targetType)
                curSceneType = targetType;

            if (curSceneType > eSceneType.NONE && curUIType.HasFlag(curSceneType))
				ReuseAnim();               
            else
                UnuseAnim();

            if (uiChildrens == null || uiChildrens.Count < 1)
                return;

            var count = uiChildrens.Count;
            for (var i = 0; i < count; ++i)
            {
                if (uiChildrens[i] == null)
                    continue;

                uiChildrens[i].InitializeUI(curSceneType);
            }
        }
        public abstract void RefreshUI();
        public virtual bool RefreshUI(eSceneType targetType) //타입 갱신부는 아래에서 상속으로 구현
        {
            if (uiChildrens == null || uiChildrens.Count < 1)
                return curSceneType != targetType;

            var count = uiChildrens.Count;
            for (var i = 0; i < count; ++i)
            {
                if (uiChildrens[i] == null)
                    continue;

                uiChildrens[i].RefreshUI(targetType);
            }

            return curSceneType != targetType;
        }

        public virtual void ShowEvent()
        {
            ReuseAnim();
        }
        public virtual void HideEvent()
        {
            UnuseAnim();
        }
        protected virtual void ReuseAnim() { gameObject.SetActive(true); } //연출 필요한 경우 아래에서 상속으로 구현.
        protected virtual void UnuseAnim() { gameObject.SetActive(false); } //연출 필요한 경우 아래에서 상속으로 구현.
    }
}