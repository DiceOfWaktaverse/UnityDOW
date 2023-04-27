using System.Collections;
using UnityEngine;

namespace DOW
{
    public abstract class Popup<T> : PopupBase where T : class
    {
        protected int order = 0;
        protected Animator popupActionAnim = null;
        protected bool dimClose = true;
        protected Coroutine openAnimation = null;

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        protected T Data { get; private set; } = null;

        public virtual void Initialize(T data)
        {
            Data = data;

            if (openAnimation != null)
                StopCoroutine(openAnimation);

            openAnimation = StartCoroutine(OpenAnimation());
        }
        public abstract void InitializeUI();
        public virtual void ForceUpdate(T data) 
        {
            DataRefresh(data);
        }

        public virtual void DataRefresh(T data)
        {
            if (data == null)
                return;

            Data = data;
        }

        protected virtual IEnumerator OpenAnimation()
        {
            dimClose = false;

            InitializeUI();

            if (popupActionAnim == null)
            {
                popupActionAnim = GetComponent<Animator>();
            }

            if (popupActionAnim != null)
                popupActionAnim.Play("PopupOpen", 0);

            yield return new WaitForSeconds(0.5f);

            dimClose = true;
        }
        protected virtual IEnumerator CloseAnimation()
        {
            if (popupActionAnim == null)
            {
                popupActionAnim = GetComponent<Animator>();
            }

            if (popupActionAnim != null)
            {
                popupActionAnim.Play("PopupClose", 0);
                yield return new WaitUntil(() => popupActionAnim.GetCurrentAnimatorStateInfo(0).IsName("PopupClose"));
                yield return new WaitUntil(() => popupActionAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            }
            SetActive(false);
            yield break;
        }
        public override void Close()
        {
            if (this != null && gameObject != null && gameObject.activeInHierarchy)
            {
                if (openAnimation != null)
                    StopCoroutine(openAnimation);

                openAnimation = StartCoroutine(CloseAnimation());
            }
        }
        public override int GetOrder()
        {
            return Order;
        }

        public override void ClosePopup() 
        {
            Data = null;
            PopupManager.RemovePopupList(this);
            Close();
        }

        public override void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
