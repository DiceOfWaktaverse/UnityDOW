using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class BattlestageSettingPopup : Popup<BattlestageSettingPopup>
    {
        [SerializeField]
        private GameObject ConfirmPopup;
        public static void OnClickSetting()
        {
            //Setting
        }

        public static void OnClickRestartStage()
        {
            //ReStartStage
        }
        public void OnClickReturnMainMenu()
        {
            ConfirmPopup.SetActive(true);
        }
        public void OnClickConfirmRetrun()
        {
            ConfirmPopup.SetActive(false);
            LoadingManager.Instance.LoadScene("StartMenu");
        }
        public void OnClickDenyRetrun()
        {
            ConfirmPopup.SetActive(false);
        }
        public static void OpenPopup()
        {
            BattlestageSettingPopup popup = PopupManager.OpenPopup<BattlestageSettingPopup>("BattlestageSettingPopup");
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
