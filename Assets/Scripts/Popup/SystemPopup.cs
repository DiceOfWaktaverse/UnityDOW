using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW { 
    public class SystemPopupData //다른 데이터가 필요할 경우 사용예정.
    {
    }
    public class SystemPopup : Popup<SystemPopupData>
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text bodyText;
        [SerializeField]
        private Text okText;
        [SerializeField]
        private Text cancleText;
        [SerializeField]
        private GameObject okBtn;
        [SerializeField]
        private GameObject cancleBtn;
        
        protected Action okCall= null;
        protected Action cancelCall = null;

        public override void Initialize() { }
        public override void InitializeUI()
        {
            cancleBtn.SetActive(false);
            ClearCallback();
        }

        public override void Refresh() { }
        public static SystemPopup OpenPopup(string title, string body, string yes, string no = "", Action okCallBack = null, Action cancelCallBack = null)
        {
            SystemPopup popup = PopupManager.OpenPopup<SystemPopup>("SystemPopup");

            popup.SetMessage(title, body, yes, no);
            popup.SetCallBack(okCallBack, cancelCallBack);

            return popup;
        }

        public static SystemPopup OpenPopup(string title, string body, Action okCallBack = null, Action cancelCallBack = null)
        {
            return OpenPopup(title, body, "", "", okCallBack, cancelCallBack);
        }
        public static SystemPopup OpenPopup(string title, string body)
        {
            var popup = OpenPopup(title, body, "", "");
            return popup;
        }

        public void SetCallBack(Action okCallBack = null, Action cancelCallBack = null) //callback 에 null 넣으면 ok 아닌 버튼들은 사라집니다
        {
            ClearCallback();

            okBtn.SetActive(true);
            if (okCallBack != null)
                okCall = okCallBack;

            if (cancelCallBack != null)
                cancelCall = cancelCallBack;

        }
        protected void ClearCallback()
        {
            okCall = null;
            cancelCall = null;
        }
        public virtual void ClickOkCall()
        {
            if (okCall == null)
            {
                ClosePopup();
                return;
            }
            okCall();
            ClosePopup();
        }
        public virtual void ClickCancelCall()
        {
            if (cancelCall == null)
            {
                ClosePopup();
                return;
            }
            cancelCall();
            ClosePopup();
        }
        public virtual void SetMessage(string title="", string body="", string yes="", string no="")
        {
            titleText.text = title;
            bodyText.text = body;
            if (yes != "")
            {
                okText.text = yes;
            }
            else
            {
                yes = "OK";//차후 테이블에서 읽는 형식이 필요해보임
                if (yes != "")
                    okText.text = yes;
            }
            if (no != "")
            {
                cancleText.text = no;
                cancleBtn.SetActive(true);
            }
            else
            {
                no = "Cancle";//차후 테이블에서 읽는 형식이 필요해보임
                if (no != "")
                    cancleText.text = no;

                cancleBtn.SetActive(false);
            }
        }
    }
}
