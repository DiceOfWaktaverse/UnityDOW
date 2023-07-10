using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    // 팝업이 닫힐 때 발생하는 이벤트, 
    // confirmed -> 확인 버튼을 눌러서 닫혔는지, 아니면 취소 버튼을 눌러서 닫혔는지
    // cards -> 카드를 선택했을 때, 선택한 카드들의 리스트
    public struct MulliganPopupClose 
    {
        public bool confirmed;
        public List<Card> cards;

        public MulliganPopupClose(bool confirmed, List<Card> cards)
        {
            this.confirmed = confirmed;
            this.cards = cards;
        }
    }

    public class MulliganPopup : Popup<MulliganPopup>
    {
        public static MulliganPopup OpenPopup()
        {
            MulliganPopup popup = PopupManager.OpenPopup<MulliganPopup>("MulliganPopup");
            return popup;
        }

        public override void Initialize()
        {

        }

        public override void InitializeUI()
        {

        }

        public override void Refresh()
        {

        }
    }
}
