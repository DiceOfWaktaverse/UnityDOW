using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class BattleEndPopup : Popup<BattleEndPopup>
    {
        public enum Status
        {
            Victory,
            Defeat
        }

        public Text resultText;

        public Transform PlaytimeText;

        public Transform RewardText;
        public Transform Reward;

        private void OnEnable()
        {
            setEndResult(Status.Victory);
        }
        public void setEndResult(Status status)
        {
            if (status == Status.Victory)
            {
                resultText.text = "�¸�";
                PlaytimeText.GetComponent<RectTransform>().localPosition = new Vector2(0, 90);
                RewardText.gameObject.SetActive(true);
                RewardText.gameObject.SetActive(true);
            }
            else if (status == Status.Defeat)
            {
                resultText.text = "�й�";
                PlaytimeText.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                RewardText.gameObject.SetActive(false);
                RewardText.gameObject.SetActive(false);

            }
        }

        //popup
        public static void OpenPopup()
        {
            PopupManager.OpenPopup<BattleEndPopup>("BattleEndPopup");
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeUI()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
