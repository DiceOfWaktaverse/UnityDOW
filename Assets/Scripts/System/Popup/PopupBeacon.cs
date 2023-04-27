using UnityEngine;

namespace DOW
{
    public class PopupBeacon : MonoBehaviour
    {
        [SerializeField]
        private GameObject popupTarget = null;
        protected virtual void Start()
        {
            // 일반 팝업
            if(popupTarget == null)
                PopupManager.Instance.SetBeacon(gameObject);
            else
                PopupManager.Instance.SetBeacon(popupTarget);
        }
    }
}
