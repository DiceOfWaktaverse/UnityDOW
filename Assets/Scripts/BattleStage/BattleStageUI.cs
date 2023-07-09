using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{

    public class BattleStageUI : UIObject
    {
        public override void InitializeUI(eSceneType targetType) {
            Debug.Log("BattleStageUI InitializeUI");
            if (eSceneType.BATTLE_STAGE == targetType)
            {
                curSceneType = targetType;
                curUIType = eSceneType.BATTLE_STAGE;
                ReuseAnim();
            }
            else
            {
                curSceneType = targetType;
                curUIType = eSceneType.NONE;
                UnuseAnim();
            }
        }

        public static void OnClickShop()
        {
            ShopPopup.OpenPopup();
        }
        public static void OnClickCoffin()
        {
            CoffinPopup.OpenPopup();
        }
        public static void OnClickPreference()
        {
            BattlestageSettingPopup.OpenPopup();
        }

        public static void OnClickTurnEnd()
        {
            BattleEndPopup.OpenPopup();
        }

        public override void RefreshUI() {

        }

        public override bool RefreshUI(eSceneType targetType)
        {
            return base.RefreshUI(targetType);
        }
    }

}