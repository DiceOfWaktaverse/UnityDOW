using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DOW
{
    public class PopupManager : Singleton<PopupManager>, IManagerBase
    {
        public Transform Beacon { get; private set; } = null;
        private Dictionary<Type, PopupBase> popupDic = null;
        private List<PopupBase> openPopupList = null;

        public static int OpenPopupCount
        {
            get
            {
                if (Instance == null || Instance.openPopupList == null)
                    return 0;

                return Instance.openPopupList.Count;
            }
        }


        public override void Initialize()
        {
            if (popupDic == null)
                popupDic = new Dictionary<Type, PopupBase>();
            else
                popupDic.Clear();

            if (openPopupList == null)
                openPopupList = new List<PopupBase>();
            else
                openPopupList.Clear();
        }

        public void SetBeacon(GameObject beacon)
        {
            if (beacon == null)
                return;

            Beacon = beacon.transform;
        }

        public void Update(float dt) { }

        public static T GetPopup<T>() where T : Component, IPopup
        {
            if (Instance.popupDic == null || Instance.popupDic.Count < 1)
                return null;

            var t = typeof(T);
            if (!Instance.popupDic.ContainsKey(t))
                return null;

            return Instance.popupDic[t] as T;
        }

        public static PopupBase CreatePopup(GameObject popup)
        {
            if (Instance.Beacon == null || Instance.popupDic == null || popup == null)
                return null;

            var target = UnityEngine.Object.Instantiate(popup, Instance.Beacon);
            if (target == null)
                return null;

            //레이어 세팅 필요
            Func.SetLayer(target, LayerMask.NameToLayer("UI"));
            //

            PopupBase result = target.GetComponent<PopupBase>();
            if (result == null || Instance.popupDic.ContainsKey(result.GetType()))
            {
                UnityEngine.Object.Destroy(target);
                return null;
            }

            Instance.popupDic.Add(result.GetType(), result);
            result.SetActive(false);
            return result;
        }


        public static T OpenPopup<T>(string path) where T : PopupBase
        {
            Type curType = typeof(T);
            T popupObj;
            if (Instance.popupDic.Keys.Contains(curType) == false)
                popupObj = CreatePopup(ResourceManager.GetResource<GameObject>(path)) as T;
            else   // 이미 로드한 팝업을 건드는 경우  
                popupObj = Instance.popupDic[curType] as T;

            if (popupObj == null)
                return null;

            popupObj.SetActive(true);
            popupObj.Initialize();
            popupObj.transform.SetAsLastSibling();

            if (Instance.openPopupList.Contains(popupObj))
                Instance.openPopupList.Remove(popupObj);

            Instance.openPopupList.Add(popupObj);
            return popupObj;
        }

        public static void RemovePopupList(PopupBase target)
        {
            if (Instance.openPopupList.Contains(target))
                Instance.openPopupList.Remove(target);
        }

        public static bool ClosePopup<T>() where T : PopupBase
        {
            return ClosePopup(GetPopup<T>());
        }

        protected static bool ClosePopup(PopupBase target)
        {
            if (target == null)
                return false;

            target.ClosePopup();
            return true;
        }

        public static void Refresh<POPUP_TYPE>() where POPUP_TYPE : PopupBase
        {
            var popup = GetPopup<POPUP_TYPE>();
            if (popup != null)
                popup.Refresh();
        }

        public static bool AllClosePopup()
        {
            if (Instance.openPopupList == null || Instance.openPopupList.Count < 1)
                return false;

            var count = Instance.openPopupList.Count;
            for(var i = 0; i < count; ++i)
            {
                if (Instance.openPopupList[i] == null)
                    continue;

                Instance.openPopupList[i].Close();
            }

            foreach(PopupBase popup in Instance.openPopupList)
            {
                popup.gameObject.SetActive(false);
            }

            Instance.openPopupList.Clear();

            return true;
        }

        public void RefreshOrder()
        {
            if (Instance.openPopupList == null)
                return;

            Instance.openPopupList.Sort(OrderSort);
            var count = Instance.openPopupList.Count;
            for(var i = 0; i < count; ++i)
            {
                (Instance.openPopupList[i])?.transform.SetAsLastSibling();
            }
        }

        private int OrderSort(IPopup p1, IPopup p2)
        {
            if (p1.GetOrder() > p2.GetOrder()) return -1;
            else if (p1.GetOrder() < p2.GetOrder()) return 1;
            return 0;
        }

		public static PopupBase GetFirstPopup()
		{
			int count = Instance.openPopupList.Count;
			if (count == 0)
                return null;

            Instance.RefreshOrder();
			return Instance.openPopupList[count - 1];
		}

        public static bool IsPopupOpening()
        {
            if (Instance.openPopupList == null)
                return false;

            return Instance.openPopupList.Count > 0;
        }
    }
}
