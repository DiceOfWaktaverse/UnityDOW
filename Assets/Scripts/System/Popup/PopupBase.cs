using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public abstract class PopupBase : MonoBehaviour, IPopup
    {
        private void Start()
        {
            Initialize();
        }
        /// <summary>
        /// 해당 Order 반환 특히 앞에 와야하는 경우에만 사용
        /// </summary>
        /// <returns>Order</returns>
        abstract public int GetOrder();
        /// <summary>
        /// 최초 1회 초기화 용도
        /// </summary>
        abstract public void Initialize();
        /// <summary>
        /// 데이터가 바뀌거나 Dirty 플레그가 새워져서 화면을 갱신해야할 경우 ex) language 변경 등
        /// </summary>
        abstract public void Refresh();
        /// <summary>
        /// 팝업을 닫는 용도
        /// </summary>
        abstract public void ClosePopup();
        /// <summary>
        /// 팝업 연출 완료 이후 실제로 꺼질 때 사용
        /// </summary>
        abstract public void Close();
        /// <summary>
        /// 외부에서 팝업을 잠시 끄기 위함.
        /// </summary>
        /// <param name="v">팝업의 켜기끄기 여부</param>
        abstract public void SetActive(bool v);
    }
}